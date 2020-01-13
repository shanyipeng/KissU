﻿using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace KissU.Modules.IdentityServer.DbMigrator
{
    public class Config
    {
        public static IEnumerable<Client> Clients = new Client[]
        {
            ///////////////////////////////////////////
            // Console Client Credentials Flow Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "client",
                ClientSecrets =
                {
                    new Secret("secret".ToSha256())
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "api1", "api2.read_only" },
                Properties =
                {
                    { "foo", "bar" }
                }
            },

            ///////////////////////////////////////////
            // Console Client Credentials Flow with client JWT assertion
            //////////////////////////////////////////
            new Client
            {
                ClientId = "client.jwt",
                ClientSecrets =
                {
                    new Secret
                    {
                        Type = IdentityServerConstants.SecretTypes.X509CertificateBase64,
                        Value = "MIIDATCCAe2gAwIBAgIQoHUYAquk9rBJcq8W+F0FAzAJBgUrDgMCHQUAMBIxEDAOBgNVBAMTB0RldlJvb3QwHhcNMTAwMTIwMjMwMDAwWhcNMjAwMTIwMjMwMDAwWjARMQ8wDQYDVQQDEwZDbGllbnQwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDSaY4x1eXqjHF1iXQcF3pbFrIbmNw19w/IdOQxbavmuPbhY7jX0IORu/GQiHjmhqWt8F4G7KGLhXLC1j7rXdDmxXRyVJBZBTEaSYukuX7zGeUXscdpgODLQVay/0hUGz54aDZPAhtBHaYbog+yH10sCXgV1Mxtzx3dGelA6pPwiAmXwFxjJ1HGsS/hdbt+vgXhdlzud3ZSfyI/TJAnFeKxsmbJUyqMfoBl1zFKG4MOvgHhBjekp+r8gYNGknMYu9JDFr1ue0wylaw9UwG8ZXAkYmYbn2wN/CpJl3gJgX42/9g87uLvtVAmz5L+rZQTlS1ibv54ScR2lcRpGQiQav/LAgMBAAGjXDBaMBMGA1UdJQQMMAoGCCsGAQUFBwMCMEMGA1UdAQQ8MDqAENIWANpX5DZ3bX3WvoDfy0GhFDASMRAwDgYDVQQDEwdEZXZSb290ghAsWTt7E82DjU1E1p427Qj2MAkGBSsOAwIdBQADggEBADLje0qbqGVPaZHINLn+WSM2czZk0b5NG80btp7arjgDYoWBIe2TSOkkApTRhLPfmZTsaiI3Ro/64q+Dk3z3Kt7w+grHqu5nYhsn7xQFAQUf3y2KcJnRdIEk0jrLM4vgIzYdXsoC6YO+9QnlkNqcN36Y8IpSVSTda6gRKvGXiAhu42e2Qey/WNMFOL+YzMXGt/nDHL/qRKsuXBOarIb++43DV3YnxGTx22llhOnPpuZ9/gnNY7KLjODaiEciKhaKqt/b57mTEz4jTF4kIg6BP03MUfDXeVlM1Qf1jB43G2QQ19n5lUiqTpmQkcfLfyci2uBZ8BkOhXr3Vk9HIk/xBXQ="
                    }
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "api1", "api2.read_only" }
            },

            ///////////////////////////////////////////
            // Custom Grant Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "client.custom",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = { "custom", "custom.nosubject" },
                AllowedScopes = { "api1", "api2.read_only" }
            },

            ///////////////////////////////////////////
            // Console Resource Owner Flow Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "roclient",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    "custom.profile",
                    "api1", "api2.read_only"
                }
            },

            ///////////////////////////////////////////
            // Console Public Resource Owner Flow Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "roclient.public",
                RequireClientSecret = false,

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    "api1", "api2.read_only"
                }
            },

            ///////////////////////////////////////////
            // Console Hybrid with PKCE Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "console.hybrid.pkce",
                ClientName = "Console Hybrid with PKCE Sample",
                RequireClientSecret = false,

                AllowedGrantTypes = GrantTypes.Hybrid,
                RequirePkce = true,

                RedirectUris = { "http://127.0.0.1" },

                AllowOfflineAccess = true,

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api1", "api2.read_only"
                }
            },

            ///////////////////////////////////////////
            // Introspection Client Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "roclient.reference",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = { "api1", "api2.read_only" },

                AccessTokenType = AccessTokenType.Reference
            },

            ///////////////////////////////////////////
            // MVC Implicit Flow Samples
            //////////////////////////////////////////
            new Client
            {
                ClientId = "mvc.implicit",
                ClientName = "MVC Implicit",
                ClientUri = "http://identityserver.io",

                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris =  { "http://localhost:44077/signin-oidc" },
                FrontChannelLogoutUri = "http://localhost:44077/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:44077/signout-callback-oidc" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api1", "api2.read_only"
                }
            },

            ///////////////////////////////////////////
            // MVC Manual Implicit Flow Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "mvc.manual",
                ClientName = "MVC Manual",
                ClientUri = "http://identityserver.io",

                AllowedGrantTypes = GrantTypes.Implicit,

                RedirectUris = { "http://localhost:44078/home/callback" },
                FrontChannelLogoutUri = "http://localhost:44078/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:44078/" },

                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId }
            },

            ///////////////////////////////////////////
            // MVC Hybrid Flow Samples
            //////////////////////////////////////////
            new Client
            {
                ClientId = "mvc.hybrid",
                ClientName = "MVC Hybrid",
                ClientUri = "http://identityserver.io",
                //LogoUri = "https://pbs.twimg.com/profile_images/1612989113/Ki-hanja_400x400.png",

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.Hybrid,
                AllowAccessTokensViaBrowser = false,

                RedirectUris = { "http://localhost:21402/signin-oidc" },
                FrontChannelLogoutUri = "http://localhost:21402/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:21402/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api1", "api2.read_only"
                }
            },

            ///////////////////////////////////////////
            // JS OAuth 2.0 Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "js_oauth",
                ClientName = "JavaScript OAuth 2.0 Client",
                ClientUri = "http://identityserver.io",
                //LogoUri = "https://pbs.twimg.com/profile_images/1612989113/Ki-hanja_400x400.png",

                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { "http://localhost:28895/index.html" },
                AllowedScopes = { "api1", "api2.read_only" }
            },
                
            ///////////////////////////////////////////
            // JS OIDC Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "js_oidc",
                ClientName = "JavaScript OIDC Client",
                ClientUri = "http://identityserver.io",
                //LogoUri = "https://pbs.twimg.com/profile_images/1612989113/Ki-hanja_400x400.png",

                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RequireClientSecret = false,
                AccessTokenType = AccessTokenType.Jwt,

                RedirectUris =
                {
                    "http://localhost:7017/index.html",
                    "http://localhost:7017/callback.html",
                    "http://localhost:7017/silent.html",
                    "http://localhost:7017/popup.html"
                },

                PostLogoutRedirectUris = { "http://localhost:7017/index.html" },
                AllowedCorsOrigins = { "http://localhost:7017" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api1", "api2.read_only", "api2.full_access"
                }
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources = new IdentityResource[]
        {
                // some standard scopes from the OIDC spec
                new IdentityResource(IdentityServerConstants.StandardScopes.OpenId,"Your user identifier",new[]
                {
                    JwtClaimTypes.Subject
                })
                {
                   Required = true
                },

                new IdentityResource(IdentityServerConstants.StandardScopes.Profile,"User profile",new[]
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.FamilyName,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.MiddleName,
                    JwtClaimTypes.NickName,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Profile,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.WebSite,
                    JwtClaimTypes.Gender,
                    JwtClaimTypes.BirthDate,
                    JwtClaimTypes.ZoneInfo,
                    JwtClaimTypes.Locale,
                    JwtClaimTypes.UpdatedAt
                })
                {
                        Description = "Your user profile information (first name, last name, etc.)",
                        Emphasize = true,

                },

                new IdentityResource(IdentityServerConstants.StandardScopes.Email,"Your email address",new[]
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                })
                {
                        Emphasize = true,
                },

                // custom identity resource with some consolidated claims
                new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location" })
        };

        public static IEnumerable<ApiResource> ApiResources = new ApiResource[]
        {
                // simple version with ctor
                new ApiResource("api1", "Some API 1")
                {
                    // this is needed for introspection when using reference tokens
                    ApiSecrets = { new Secret("secret".Sha256()) }
                },
                
                // expanded version if more control is needed
                new ApiResource
                {
                    Name = "api2",

                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },

                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "api2.full_access",
                            DisplayName = "Full access to API 2"
                        },
                        new Scope
                        {
                            Name = "api2.read_only",
                            DisplayName = "Read only access to API 2"
                        }
                }
            }
        };
    }
}
