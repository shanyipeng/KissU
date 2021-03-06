using KissU.Modules.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace KissU.Modules.Identity.EntityFrameworkCore
{
    [ConnectionStringName(AbpIdentityDbProperties.ConnectionStringName)]
    public interface IIdentityDbContext : IEfCoreDbContext
    {
        DbSet<IdentityUser> Users { get; set; }

        DbSet<IdentityRole> Roles { get; set; }

        DbSet<IdentityClaimType> ClaimTypes { get; set; }

        DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    }
}
