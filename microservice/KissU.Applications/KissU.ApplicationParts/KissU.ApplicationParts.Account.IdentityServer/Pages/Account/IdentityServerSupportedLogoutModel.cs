﻿using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using KissU.ApplicationParts.Account.Pages.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace KissU.ApplicationParts.Account.IdentityServer.Pages.Account
{
    [ExposeServices(typeof(LogoutModel))]
    public class IdentityServerSupportedLogoutModel : LogoutModel
    {
        protected IIdentityServerInteractionService Interaction { get; }

        public IdentityServerSupportedLogoutModel(IIdentityServerInteractionService interaction)
        {
            Interaction = interaction;
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            await SignInManager.SignOutAsync();

            var logoutId = Request.Query["logoutId"].ToString();

            if (!string.IsNullOrEmpty(logoutId))
            {
                var logoutContext = await Interaction.GetLogoutContextAsync(logoutId);
                await SignInManager.SignOutAsync();

                HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

                LoggedOutModel vm = new LoggedOutModel()
                {
                    PostLogoutRedirectUri = logoutContext?.PostLogoutRedirectUri,
                    ClientName = logoutContext?.ClientName,
                    SignOutIframeUrl = logoutContext?.SignOutIFrameUrl
                };

                Logger.LogInformation($"Redirecting to LoggedOut Page...");

                return RedirectToPage("./LoggedOut", vm);
            }

            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }

            Logger.LogInformation(
                $"IdentityServerSupportedLogoutModel couldn't find postLogoutUri... Redirecting to:/Account/Login..");
            return RedirectToPage("/Account/Login");
        }
    }
}
