﻿using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace KissU.Modules.IdentityServer.Domain.Shared.ObjectExtending
{
    public class IdentityServerModuleExtensionConfiguration : ModuleExtensionConfiguration
    {
        public IdentityServerModuleExtensionConfiguration ConfigureClient(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                IdentityServerModuleExtensionConsts.EntityNames.Client,
                configureAction
            );
        }

        public IdentityServerModuleExtensionConfiguration ConfigureApiResource(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                IdentityServerModuleExtensionConsts.EntityNames.ApiResource,
                configureAction
            );
        }

        public IdentityServerModuleExtensionConfiguration ConfigureIdentityResource(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                IdentityServerModuleExtensionConsts.EntityNames.IdentityResource,
                configureAction
            );
        }
    }
}
