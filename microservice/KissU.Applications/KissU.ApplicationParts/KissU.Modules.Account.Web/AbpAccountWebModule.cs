﻿using KissU.Modules.Account.Application.Contracts.Localization;
using KissU.Modules.Account.Service.Contracts;
using KissU.Modules.Identity.AspNetCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace KissU.Modules.Account.Web
{
    [DependsOn(
        typeof(AccountServiceContractsModule),
        typeof(AbpIdentityAspNetCoreModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule)
        )]
    public class AbpAccountWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AccountResource), typeof(AbpAccountWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpAccountWebModule>("KissU.Modules.Account.Web");
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new AbpAccountUserMenuContributor());
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new AccountModuleToolbarContributor());
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/Account/Manage");
            });

            context.Services.AddAutoMapperObjectMapper<AbpAccountWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpAccountWebAutoMapperProfile>(validate: true);
            });
        }
    }
}
