﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace KissU.Surging.Swagger.SwaggerUI
{
    /// <summary>
    /// SwaggerUIOptions.
    /// </summary>
    public class SwaggerUIOptions
    {
        /// <summary>
        /// Gets or sets a route prefix for accessing the swagger-ui
        /// </summary>
        public string RoutePrefix { get; set; } = "swagger";

        /// <summary>
        /// Gets or sets a Stream function for retrieving the swagger-ui page
        /// </summary>
        public Func<Stream> IndexStream { get; set; } = () => typeof(SwaggerUIOptions).GetTypeInfo().Assembly
            .GetManifestResourceStream("KissU.Surging.Swagger.SwaggerUI.index.html");

        /// <summary>
        /// Gets or sets a title for the swagger-ui page
        /// </summary>
        public string DocumentTitle { get; set; } = "Swagger UI";

        /// <summary>
        /// Gets or sets additional content to place in the head of the swagger-ui page
        /// </summary>
        public string HeadContent { get; set; } = "";

        /// <summary>
        /// Gets the JavaScript config object, represented as JSON, that will be passed to the SwaggerUI
        /// </summary>
        public ConfigObject ConfigObject { get; set; } = new ConfigObject();

        /// <summary>
        /// Gets the JavaScript config object, represented as JSON, that will be passed to the initOAuth method
        /// </summary>
        public OAuthConfigObject OAuthConfigObject { get; set; } = new OAuthConfigObject();
    }

    /// <summary>
    /// ConfigObject.
    /// </summary>
    public class ConfigObject
    {
        /// <summary>
        /// The additional items
        /// </summary>
        [JsonExtensionData] public Dictionary<string, object> AdditionalItems = new Dictionary<string, object>();

        /// <summary>
        /// One or more Swagger JSON endpoints (url and name) to power the UI
        /// </summary>
        public IEnumerable<UrlDescriptor> Urls { get; set; } = null;

        /// <summary>
        /// If set to true, enables deep linking for tags and operations
        /// </summary>
        public bool DeepLinking { get; set; } = false;

        /// <summary>
        /// Controls the display of operationId in operations list
        /// </summary>
        public bool DisplayOperationId { get; set; } = false;

        /// <summary>
        /// The default expansion depth for models (set to -1 completely hide the models)
        /// </summary>
        public int DefaultModelsExpandDepth { get; set; } = 1;

        /// <summary>
        /// The default expansion depth for the model on the model-example section
        /// </summary>
        public int DefaultModelExpandDepth { get; set; } = 1;

        /// <summary>
        /// Controls how the model is shown when the API is first rendered.
        /// (The user can always switch the rendering for a given model by clicking the 'Model' and 'Example Value' links)
        /// </summary>
        public ModelRendering DefaultModelRendering { get; set; } = ModelRendering.Example;

        /// <summary>
        /// Controls the display of the request duration (in milliseconds) for Try-It-Out requests
        /// </summary>
        public bool DisplayRequestDuration { get; set; } = false;

        /// <summary>
        /// Controls the default expansion setting for the operations and tags.
        /// It can be 'list' (expands only the tags), 'full' (expands the tags and operations) or 'none' (expands nothing)
        /// </summary>
        public DocExpansion DocExpansion { get; set; } = DocExpansion.List;

        /// <summary>
        /// If set, enables filtering. The top bar will show an edit box that you can use to filter the tagged operations
        /// that are shown. Can be an empty string or specific value, in which case filtering will be enabled using that
        /// value as the filter expression. Filtering is case sensitive matching the filter expression anywhere inside the tag
        /// </summary>
        public string Filter { get; set; } = null;

        /// <summary>
        /// If set, limits the number of tagged operations displayed to at most this many. The default is to show all operations
        /// </summary>
        public int? MaxDisplayedTags { get; set; } = null;

        /// <summary>
        /// Controls the display of vendor extension (x-) fields and values for Operations, Parameters, and Schema
        /// </summary>
        public bool ShowExtensions { get; set; } = false;

        /// <summary>
        /// Controls the display of extensions (pattern, maxLength, minLength, maximum, minimum) fields and values for Parameters
        /// </summary>
        public bool ShowCommonExtensions { get; set; } = false;

        /// <summary>
        /// OAuth redirect URL
        /// </summary>
        [JsonProperty("oauth2RedirectUrl")]
        public string OAuth2RedirectUrl { get; set; }

        /// <summary>
        /// List of HTTP methods that have the Try it out feature enabled.
        /// An empty array disables Try it out for all operations. This does not filter the operations from the display
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public IEnumerable<SubmitMethod> SupportedSubmitMethods { get; set; } =
            Enum.GetValues(typeof(SubmitMethod)).Cast<SubmitMethod>();

        /// <summary>
        /// By default, Swagger-UI attempts to validate specs against swagger.io's online validator.
        /// You can use this parameter to set a different validator URL, for example for locally deployed validators (Validator
        /// Badge).
        /// Setting it to null will disable validation
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string ValidatorUrl { get; set; }
    }

    /// <summary>
    /// UrlDescriptor.
    /// </summary>
    public class UrlDescriptor
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Enum ModelRendering
    /// </summary>
    public enum ModelRendering
    {
        /// <summary>
        /// The example
        /// </summary>
        Example,

        /// <summary>
        /// The model
        /// </summary>
        Model
    }

    /// <summary>
    /// Enum DocExpansion
    /// </summary>
    public enum DocExpansion
    {
        /// <summary>
        /// The list
        /// </summary>
        List,

        /// <summary>
        /// The full
        /// </summary>
        Full,

        /// <summary>
        /// The none
        /// </summary>
        None
    }

    /// <summary>
    /// Enum SubmitMethod
    /// </summary>
    public enum SubmitMethod
    {
        /// <summary>
        /// The get
        /// </summary>
        Get,

        /// <summary>
        /// The put
        /// </summary>
        Put,

        /// <summary>
        /// The post
        /// </summary>
        Post,

        /// <summary>
        /// The delete
        /// </summary>
        Delete,

        /// <summary>
        /// The options
        /// </summary>
        Options,

        /// <summary>
        /// The head
        /// </summary>
        Head,

        /// <summary>
        /// The patch
        /// </summary>
        Patch,

        /// <summary>
        /// The trace
        /// </summary>
        Trace
    }

    /// <summary>
    /// OAuthConfigObject.
    /// </summary>
    public class OAuthConfigObject
    {
        /// <summary>
        /// Default clientId
        /// </summary>
        public string ClientId { get; set; } = "clientId";

        /// <summary>
        /// Default clientSecret
        /// </summary>
        public string ClientSecret { get; set; } = "clientSecret";

        /// <summary>
        /// Realm query parameter (for oauth1) added to authorizationUrl and tokenUrl
        /// </summary>
        public string Realm { get; set; } = null;

        /// <summary>
        /// Application name, displayed in authorization popup
        /// </summary>
        public string AppName { get; set; } = null;

        /// <summary>
        /// Scope separator for passing scopes, encoded before calling, default value is a space (encoded value %20)
        /// </summary>
        public string ScopeSeperator { get; set; } = " ";

        /// <summary>
        /// Additional query parameters added to authorizationUrl and tokenUrl
        /// </summary>
        public Dictionary<string, string> AdditionalQueryStringParams { get; set; } = null;

        /// <summary>
        /// Only activated for the accessCode flow. During the authorization_code request to the tokenUrl,
        /// pass the Client Password using the HTTP Basic Authentication scheme
        /// (Authorization header with Basic base64encode(client_id + client_secret))
        /// </summary>
        public bool UseBasicAuthenticationWithAccessCodeGrant { get; set; } = false;
    }
}