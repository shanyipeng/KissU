﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KissU.Core.CPlatform.Messages;
using KissU.Core.CPlatform.Routing;
using KissU.Core.CPlatform.Routing.Template;
using KissU.Core.CPlatform.Serialization;
using KissU.Core.CPlatform.Transport;
using KissU.Core.CPlatform.Transport.Implementation;
using KissU.Core.KestrelHttpServer.Filters;
using KissU.Core.KestrelHttpServer.Filters.Implementation;
using KissU.Core.KestrelHttpServer.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace KissU.Core.KestrelHttpServer
{
    /// <summary>
    /// HttpMessageListener.
    /// Implements the <see cref="KissU.Core.CPlatform.Transport.IMessageListener" />
    /// </summary>
    /// <seealso cref="KissU.Core.CPlatform.Transport.IMessageListener" />
    public abstract class HttpMessageListener : IMessageListener
    {
        private readonly ILogger<HttpMessageListener> _logger;
        private readonly ISerializer<string> _serializer;
        private readonly string[] _serviceKeys = {"serviceKey", "servicekey"};
        private readonly IServiceRouteProvider _serviceRouteProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMessageListener" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="serviceRouteProvider">The service route provider.</param>
        public HttpMessageListener(ILogger<HttpMessageListener> logger, ISerializer<string> serializer,
            IServiceRouteProvider serviceRouteProvider)
        {
            _logger = logger;
            _serializer = serializer;
            _serviceRouteProvider = serviceRouteProvider;
        }

        /// <summary>
        /// 接收到消息的事件。
        /// </summary>
        public event ReceivedDelegate Received;

        /// <summary>
        /// 触发接收到消息事件。
        /// </summary>
        /// <param name="sender">消息发送者。</param>
        /// <param name="message">接收到的消息。</param>
        /// <returns>一个任务。</returns>
        public async Task OnReceived(IMessageSender sender, TransportMessage message)
        {
            if (Received == null)
                return;
            await Received(sender, message);
        }

        private event RequestDelegate Requested;

        /// <summary>
        /// Called when [received].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="context">The context.</param>
        /// <param name="actionFilters">The action filters.</param>
        public async Task OnReceived(IMessageSender sender, string messageId, HttpContext context,
            IEnumerable<IActionFilter> actionFilters)
        {
            var serviceRoute = context.Items["route"] as ServiceRoute;

            var path = (context.Items["path"]
                        ?? HttpUtility.UrlDecode(GetRoutePath(context.Request.Path.ToString()))) as string;
            if (serviceRoute == null)
            {
                serviceRoute = await _serviceRouteProvider.GetRouteByPathRegex(path);
            }

            IDictionary<string, object> parameters =
                context.Request.Query.ToDictionary(p => p.Key, p => (object) p.Value.ToString());
            object serviceKey = null;
            foreach (var key in _serviceKeys)
            {
                parameters.Remove(key, out var value);
                if (value != null)
                {
                    serviceKey = value;
                    break;
                }
            }

            if (string.Compare(serviceRoute.ServiceDescriptor.RoutePath, path, true) != 0)
            {
                var @params = RouteTemplateSegmenter.Segment(serviceRoute.ServiceDescriptor.RoutePath, path);
                foreach (var param in @params)
                {
                    parameters.Add(param.Key, param.Value);
                }
            }

            var httpMessage = new HttpRequestMessage
            {
                Parameters = parameters,
                RoutePath = serviceRoute.ServiceDescriptor.RoutePath,
                ServiceKey = serviceKey?.ToString()
            };

            if (context.Request.HasFormContentType)
            {
                var collection = await GetFormCollection(context.Request);
                httpMessage.Parameters.Add("form", collection);
                if (!await OnActionExecuting(
                    new ActionExecutingContext {Context = context, Route = serviceRoute, Message = httpMessage},
                    sender, messageId, actionFilters)) return;
                httpMessage.Attachments = RpcContext.GetContext().GetContextParameters();
                await Received(sender, new TransportMessage(messageId, httpMessage));
            }
            else
            {
                var streamReader = new StreamReader(context.Request.Body);
                var data = await streamReader.ReadToEndAsync();
                if (context.Request.Method == "POST" || context.Request.Method == "PUT")
                {
                    var bodyParams = _serializer.Deserialize<string, IDictionary<string, object>>(data) ??
                                     new Dictionary<string, object>();
                    foreach (var param in bodyParams)
                        httpMessage.Parameters.Add(param.Key, param.Value);
                    if (!await OnActionExecuting(
                        new ActionExecutingContext {Context = context, Route = serviceRoute, Message = httpMessage},
                        sender, messageId, actionFilters)) return;
                    httpMessage.Attachments = RpcContext.GetContext().GetContextParameters();
                    await Received(sender, new TransportMessage(messageId, httpMessage));
                }
                else
                {
                    if (!await OnActionExecuting(
                        new ActionExecutingContext {Context = context, Route = serviceRoute, Message = httpMessage},
                        sender, messageId, actionFilters)) return;
                    httpMessage.Attachments = RpcContext.GetContext().GetContextParameters();
                    await Received(sender, new TransportMessage(messageId, httpMessage));
                }
            }

            await OnActionExecuted(context, httpMessage, actionFilters);
        }

        /// <summary>
        /// Called when [action executing].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="filters">The filters.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> OnActionExecuting(ActionExecutingContext filterContext, IMessageSender sender,
            string messageId, IEnumerable<IActionFilter> filters)
        {
            foreach (var fiter in filters)
            {
                await fiter.OnActionExecuting(filterContext);
                if (filterContext.Result != null)
                {
                    await sender.SendAndFlushAsync(new TransportMessage(messageId, filterContext.Result));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Called when [action executed].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="message">The message.</param>
        /// <param name="filters">The filters.</param>
        public async Task OnActionExecuted(HttpContext context, HttpRequestMessage message,
            IEnumerable<IActionFilter> filters)
        {
            foreach (var fiter in filters)
            {
                var filterContext = new ActionExecutedContext
                {
                    Context = context,
                    Message = message
                };
                await fiter.OnActionExecuted(filterContext);
            }
        }

        /// <summary>
        /// Called when [authorization].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="filters">The filters.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> OnAuthorization(HttpContext context, HttpServerMessageSender sender, string messageId,
            IEnumerable<IAuthorizationFilter> filters)
        {
            foreach (var filter in filters)
            {
                var path = HttpUtility.UrlDecode(GetRoutePath(context.Request.Path.ToString()));
                var serviceRoute = await _serviceRouteProvider.GetRouteByPathRegex(path);
                if (serviceRoute == null) serviceRoute = await _serviceRouteProvider.GetLocalRouteByPathRegex(path);
                context.Items.Add("route", serviceRoute);
                var filterContext = new AuthorizationFilterContext
                {
                    Path = path,
                    Context = context,
                    Route = serviceRoute
                };
                await filter.OnAuthorization(filterContext);
                if (filterContext.Result != null)
                {
                    await sender.SendAndFlushAsync(new TransportMessage(messageId, filterContext.Result));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="filters">The filters.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> OnException(HttpContext context, HttpServerMessageSender sender, string messageId,
            Exception exception, IEnumerable<IExceptionFilter> filters)
        {
            foreach (var filter in filters)
            {
                var path = HttpUtility.UrlDecode(GetRoutePath(context.Request.Path.ToString()));
                var filterContext = new ExceptionContext
                {
                    RoutePath = path,
                    Context = context,
                    Exception = exception
                };
                await filter.OnException(filterContext);
                if (filterContext.Result != null)
                {
                    await sender.SendAndFlushAsync(new TransportMessage(messageId, filterContext.Result));
                    return false;
                }
            }

            return true;
        }

        private async Task<HttpFormCollection> GetFormCollection(HttpRequest request)
        {
            var boundary = GetName("boundary=", request.ContentType);
            var reader = new MultipartReader(boundary, request.Body);
            var collection = await GetMultipartForm(reader);
            var fileCollection = new HttpFormFileCollection();
            var fields = new Dictionary<string, StringValues>();
            foreach (var item in collection)
            {
                if (item.Value is HttpFormFileCollection)
                {
                    var itemCollection = item.Value as HttpFormFileCollection;
                    fileCollection.AddRange(itemCollection);
                }
                else
                {
                    var itemCollection = item.Value as Dictionary<string, StringValues>;
                    fields = fields.Concat(itemCollection).ToDictionary(k => k.Key, v => v.Value);
                }
            }

            return new HttpFormCollection(fields, fileCollection);
        }

        private async Task<IDictionary<string, object>> GetMultipartForm(MultipartReader reader)
        {
            var section = await reader.ReadNextSectionAsync();
            var collection = new Dictionary<string, object>();
            if (section != null)
            {
                var name = GetName("name=", section.ContentDisposition);
                var fileName = GetName("filename=", section.ContentDisposition);
                var buffer = new MemoryStream();
                await section.Body.CopyToAsync(buffer);
                if (string.IsNullOrEmpty(fileName))
                {
                    var fields = new Dictionary<string, StringValues>();
                    var streamReader = new StreamReader(buffer);
                    fields.Add(name,
                        new StringValues(Encoding.Default.GetString(buffer.GetBuffer(), 0, (int) buffer.Length)));
                    collection.Add(name, fields);
                }
                else
                {
                    var fileCollection = new HttpFormFileCollection();
                    var streamReader = new StreamReader(buffer);
                    fileCollection.Add(new HttpFormFile(buffer.Length, name, fileName, buffer.GetBuffer()));
                    collection.Add(name, fileCollection);
                }

                var formCollection = await GetMultipartForm(reader);
                foreach (var item in formCollection)
                {
                    if (!collection.ContainsKey(item.Key))
                        collection.Add(item.Key, item.Value);
                }
            }

            return collection;
        }

        private string GetName(string type, string content)
        {
            var elements = content.Split(';');
            var element = elements.Where(entry => entry.Trim().StartsWith(type)).FirstOrDefault()?.Trim();
            var name = element?.Substring(type.Length);
            if (!string.IsNullOrEmpty(name) && name.Length >= 2 && name[0] == '"' && name[name.Length - 1] == '"')
            {
                name = name.Substring(1, name.Length - 2);
            }

            return name;
        }

        private string GetRoutePath(string path)
        {
            var routePath = "";
            var urlSpan = path.AsSpan();
            var len = urlSpan.IndexOf("?");
            if (urlSpan.LastIndexOf("/") == 0)
            {
                routePath = path;
            }
            else
            {
                if (len == -1)
                    routePath = urlSpan.TrimStart("/").ToString().ToLower();
                else
                    routePath = urlSpan.Slice(0, len).TrimStart("/").ToString().ToLower();
            }

            return routePath;
        }
    }
}