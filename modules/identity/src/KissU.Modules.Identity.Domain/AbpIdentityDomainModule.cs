﻿using KissU.Modules.Identity.Domain.Extensions;
using KissU.Modules.Identity.Domain.Shared;
using KissU.Modules.Identity.Domain.Shared.ObjectExtending;
using KissU.Modules.Users.Abstractions;
using KissU.Modules.Users.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending.Modularity;

namespace KissU.Modules.Identity.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpUsersDomainModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AbpIdentityDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpIdentityDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<IdentityDomainMappingProfile>(validate: true);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<IdentityUser, UserEto>(typeof(AbpIdentityDomainModule));
                options.EtoMappings.Add<IdentityClaimType, IdentityClaimTypeEto>(typeof(AbpIdentityDomainModule));
                options.EtoMappings.Add<IdentityRole, IdentityRoleEto>(typeof(AbpIdentityDomainModule));
            });
            
            var identityBuilder = context.Services.AddAbpIdentity(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            context.Services.AddObjectAccessor(identityBuilder);
            context.Services.ExecutePreConfiguredActions(identityBuilder);

            AddAbpIdentityOptionsFactory(context.Services);
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.User,
                typeof(IdentityUser)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.Role,
                typeof(IdentityRole)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.ClaimType,
                typeof(IdentityClaimType)
            );
        }

        private static void AddAbpIdentityOptionsFactory(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<IOptionsFactory<IdentityOptions>, AbpIdentityOptionsFactory>());
            services.Replace(ServiceDescriptor.Scoped<IOptions<IdentityOptions>, OptionsManager<IdentityOptions>>());
        }
    }
}