﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using KissU.Core.CPlatform.Runtime.Server;
using KissU.Core.Swagger.Swagger.Model;

namespace KissU.Core.Swagger.SwaggerUI
{
    /// <summary>
    /// SwaggerUIOptionsExtensions.
    /// </summary>
    public static class SwaggerUIOptionsExtensions
    {
        /// <summary>
        /// Injects additional CSS stylesheets into the index.html page
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="path">A path to the stylesheet - i.e. the link "href" attribute</param>
        /// <param name="media">The target media - i.e. the link "media" attribute</param>
        public static void InjectStylesheet(this SwaggerUIOptions options, string path, string media = "screen")
        {
            var builder = new StringBuilder(options.HeadContent);
            builder.AppendLine($"<link href='{path}' rel='stylesheet' media='{media}' type='text/css' />");
            options.HeadContent = builder.ToString();
        }

        /// <summary>
        /// Injects additional Javascript files into the index.html page
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="path">A path to the javascript - i.e. the script "src" attribute</param>
        /// <param name="type">The script type - i.e. the script "type" attribute</param>
        public static void InjectJavascript(this SwaggerUIOptions options, string path, string type = "text/javascript")
        {
            var builder = new StringBuilder(options.HeadContent);
            builder.AppendLine($"<script src='{path}' type='{type}'></script>");
            options.HeadContent = builder.ToString();
        }

        /// <summary>
        /// Adds Swagger JSON endpoints. Can be fully-qualified or relative to the UI page
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="url">Can be fully qualified or relative to the current host</param>
        /// <param name="name">The description that appears in the document selector drop-down</param>
        /// <param name="areaName">Name of the area.</param>
        public static void SwaggerEndpoint(this SwaggerUIOptions options, string url, string name, string areaName)
        {
            var urls = new List<UrlDescriptor>(options.ConfigObject.Urls ?? Enumerable.Empty<UrlDescriptor>());
            urls.Add(new UrlDescriptor {Url = string.IsNullOrEmpty(areaName) ? url : $"{areaName}{url}", Name = name});
            options.ConfigObject.Urls = urls;
        }

        /// <summary>
        /// Swaggers the endpoint.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="entries">The entries.</param>
        /// <param name="areaName">Name of the area.</param>
        public static void SwaggerEndpoint(this SwaggerUIOptions options, IEnumerable<ServiceEntry> entries,
            string areaName)
        {
            var list = new List<Info>();
            var assemblies = entries.Select(p => p.Type.Assembly).Distinct();
            foreach (var assembly in assemblies)
            {
                var version = assembly
                    .GetCustomAttributes(true)
                    .OfType<AssemblyFileVersionAttribute>().FirstOrDefault();

                var title = assembly
                    .GetCustomAttributes(true)
                    .OfType<AssemblyTitleAttribute>().FirstOrDefault();

                var des = assembly
                    .GetCustomAttributes(true)
                    .OfType<AssemblyDescriptionAttribute>().FirstOrDefault();

                if (version == null || title == null)
                    continue;

                var info = new Info
                {
                    Title = title.Title,
                    Version = version.Version,
                    Description = des?.Description
                };
                options.SwaggerEndpoint($"../swagger/{info.Title}/swagger.json", info.Title, areaName);
            }
        }

        /// <summary>
        /// Enables deep linking for tags and operations
        /// </summary>
        /// <param name="options">The options.</param>
        public static void EnableDeepLinking(this SwaggerUIOptions options)
        {
            options.ConfigObject.DeepLinking = true;
        }

        /// <summary>
        /// Controls the display of operationId in operations list
        /// </summary>
        /// <param name="options">The options.</param>
        public static void DisplayOperationId(this SwaggerUIOptions options)
        {
            options.ConfigObject.DisplayOperationId = true;
        }

        /// <summary>
        /// The default expansion depth for models (set to -1 completely hide the models)
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="depth">The depth.</param>
        public static void DefaultModelsExpandDepth(this SwaggerUIOptions options, int depth)
        {
            options.ConfigObject.DefaultModelsExpandDepth = depth;
        }

        /// <summary>
        /// The default expansion depth for the model on the model-example section
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="depth">The depth.</param>
        public static void DefaultModelExpandDepth(this SwaggerUIOptions options, int depth)
        {
            options.ConfigObject.DefaultModelExpandDepth = depth;
        }

        /// <summary>
        /// Controls how the model is shown when the API is first rendered.
        /// (The user can always switch the rendering for a given model by clicking the 'Model' and 'Example Value' links.)
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="modelRendering">The model rendering.</param>
        public static void DefaultModelRendering(this SwaggerUIOptions options, ModelRendering modelRendering)
        {
            options.ConfigObject.DefaultModelRendering = modelRendering;
        }

        /// <summary>
        /// Controls the display of the request duration (in milliseconds) for Try-It-Out requests
        /// </summary>
        /// <param name="options">The options.</param>
        public static void DisplayRequestDuration(this SwaggerUIOptions options)
        {
            options.ConfigObject.DisplayRequestDuration = true;
        }

        /// <summary>
        /// Controls the default expansion setting for the operations and tags.
        /// It can be 'List' (expands only the tags), 'Full' (expands the tags and operations) or 'None' (expands nothing)
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="docExpansion">The document expansion.</param>
        public static void DocExpansion(this SwaggerUIOptions options, DocExpansion docExpansion)
        {
            options.ConfigObject.DocExpansion = docExpansion;
        }

        /// <summary>
        /// Enables filtering. The top bar will show an edit box that you can use to filter the tagged operations that are shown.
        /// If an expression is provided it will be used and applied initially.
        /// Filtering is case sensitive matching the filter expression anywhere inside the tag
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="expression">The expression.</param>
        public static void EnableFilter(this SwaggerUIOptions options, string expression = null)
        {
            options.ConfigObject.Filter = expression ?? "";
        }

        /// <summary>
        /// Limits the number of tagged operations displayed to at most this many. The default is to show all operations
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="count">The count.</param>
        public static void MaxDisplayedTags(this SwaggerUIOptions options, int count)
        {
            options.ConfigObject.MaxDisplayedTags = count;
        }

        /// <summary>
        /// Controls the display of vendor extension (x-) fields and values for Operations, Parameters, and Schema
        /// </summary>
        /// <param name="options">The options.</param>
        public static void ShowExtensions(this SwaggerUIOptions options)
        {
            options.ConfigObject.ShowExtensions = true;
        }

        /// <summary>
        /// List of HTTP methods that have the Try it out feature enabled. An empty array disables Try it out for all operations.
        /// This does not filter the operations from the display
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="submitMethods">The submit methods.</param>
        public static void SupportedSubmitMethods(this SwaggerUIOptions options, params SubmitMethod[] submitMethods)
        {
            options.ConfigObject.SupportedSubmitMethods = submitMethods;
        }

        /// <summary>
        /// OAuth redirect URL
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="url">The URL.</param>
        public static void OAuth2RedirectUrl(this SwaggerUIOptions options, string url)
        {
            options.ConfigObject.OAuth2RedirectUrl = url;
        }

        /// <summary>
        /// Validators the URL.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="url">The URL.</param>
        [Obsolete("The validator is disabled by default. Use EnableValidator to enable it")]
        public static void ValidatorUrl(this SwaggerUIOptions options, string url)
        {
            options.ConfigObject.ValidatorUrl = url;
        }

        /// <summary>
        /// You can use this parameter to enable the swagger-ui's built-in validator (badge) functionality
        /// Setting it to null will disable validation
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="url">The URL.</param>
        public static void EnableValidator(this SwaggerUIOptions options,
            string url = "https://online.swagger.io/validator")
        {
            options.ConfigObject.ValidatorUrl = url;
        }

        /// <summary>
        /// Default clientId
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="value">The value.</param>
        public static void OAuthClientId(this SwaggerUIOptions options, string value)
        {
            options.OAuthConfigObject.ClientId = value;
        }

        /// <summary>
        /// Default clientSecret
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="value">The value.</param>
        public static void OAuthClientSecret(this SwaggerUIOptions options, string value)
        {
            options.OAuthConfigObject.ClientSecret = value;
        }

        /// <summary>
        /// realm query parameter (for oauth1) added to authorizationUrl and tokenUrl
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="value">The value.</param>
        public static void OAuthRealm(this SwaggerUIOptions options, string value)
        {
            options.OAuthConfigObject.Realm = value;
        }

        /// <summary>
        /// Application name, displayed in authorization popup
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="value">The value.</param>
        public static void OAuthAppName(this SwaggerUIOptions options, string value)
        {
            options.OAuthConfigObject.AppName = value;
        }

        /// <summary>
        /// Scope separator for passing scopes, encoded before calling, default value is a space (encoded value %20)
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="value">The value.</param>
        public static void OAuthScopeSeparator(this SwaggerUIOptions options, string value)
        {
            options.OAuthConfigObject.ScopeSeperator = value;
        }

        /// <summary>
        /// Additional query parameters added to authorizationUrl and tokenUrl
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="value">The value.</param>
        public static void OAuthAdditionalQueryStringParams(
            this SwaggerUIOptions options,
            Dictionary<string, string> value)
        {
            options.OAuthConfigObject.AdditionalQueryStringParams = value;
        }

        /// <summary>
        /// Only activated for the accessCode flow. During the authorization_code request to the tokenUrl,
        /// pass the Client Password using the HTTP Basic Authentication scheme (Authorization header with
        /// Basic base64encoded[client_id:client_secret]). The default is false
        /// </summary>
        /// <param name="options">The options.</param>
        public static void OAuthUseBasicAuthenticationWithAccessCodeGrant(this SwaggerUIOptions options)
        {
            options.OAuthConfigObject.UseBasicAuthenticationWithAccessCodeGrant = true;
        }
    }
}