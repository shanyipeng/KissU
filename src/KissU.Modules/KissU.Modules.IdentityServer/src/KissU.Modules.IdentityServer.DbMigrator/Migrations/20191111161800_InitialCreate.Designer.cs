﻿// <auto-generated />
using System;
using KissU.Modules.IdentityServer.DbMigrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KissU.Modules.IdentityServer.DbMigrator.Migrations
{
    [DbContext(typeof(DesignTimeDbContext))]
    [Migration("20191111161800_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiResourceAggregate.ApiResource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ApiResourceId");

                    b.Property<string>("ClaimTypes")
                        .HasMaxLength(2000);

                    b.Property<DateTime?>("CreationTime");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("DisplayName")
                        .HasMaxLength(200);

                    b.Property<bool>("Enabled");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<Guid?>("LastModifierId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("ApiResources","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiResourceAggregate.ApiResourceScope", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApiResourceId");

                    b.Property<string>("ClaimTypes")
                        .HasMaxLength(2000);

                    b.Property<DateTime?>("CreationTime");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("DisplayName")
                        .HasMaxLength(200);

                    b.Property<bool>("Emphasize");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<Guid?>("LastModifierId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("Required");

                    b.Property<bool>("ShowInDiscoveryDocument");

                    b.HasKey("Id");

                    b.HasIndex("ApiResourceId");

                    b.ToTable("ApiResourceScopes","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiResourceAggregate.ApiResourceSecret", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApiResourceId");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("Expiration");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<Guid?>("LastModifierId");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("ApiResourceId");

                    b.ToTable("ApiResourceSecrets","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientAggregate.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientId");

                    b.Property<int>("AbsoluteRefreshTokenLifetime");

                    b.Property<int>("AccessTokenLifetime");

                    b.Property<int>("AccessTokenType");

                    b.Property<bool>("AllowAccessTokensViaBrowser");

                    b.Property<bool>("AllowOfflineAccess");

                    b.Property<bool>("AllowPlainTextPkce");

                    b.Property<bool>("AllowRememberConsent");

                    b.Property<bool>("AlwaysIncludeUserClaimsInIdToken");

                    b.Property<bool>("AlwaysSendClientClaims");

                    b.Property<int>("AuthorizationCodeLifetime");

                    b.Property<bool>("BackChannelLogoutSessionRequired");

                    b.Property<string>("BackChannelLogoutUri")
                        .HasMaxLength(2000);

                    b.Property<string>("ClientAllowedCorsOrigins")
                        .HasMaxLength(2000);

                    b.Property<string>("ClientAllowedGrantTypes");

                    b.Property<string>("ClientAllowedScopes")
                        .HasMaxLength(2000);

                    b.Property<string>("ClientClaimsPrefix")
                        .HasMaxLength(200);

                    b.Property<string>("ClientCode")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("ClientIdentityProviderRestrictions")
                        .HasMaxLength(2000);

                    b.Property<string>("ClientName")
                        .HasMaxLength(200);

                    b.Property<string>("ClientPostLogoutRedirectUris")
                        .HasMaxLength(2000);

                    b.Property<string>("ClientProperties");

                    b.Property<string>("ClientRedirectUris")
                        .HasMaxLength(2000);

                    b.Property<string>("ClientUri")
                        .HasMaxLength(2000);

                    b.Property<int?>("ConsentLifetime");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<bool>("EnableLocalLogin");

                    b.Property<bool>("Enabled");

                    b.Property<bool>("FrontChannelLogoutSessionRequired");

                    b.Property<string>("FrontChannelLogoutUri")
                        .HasMaxLength(2000);

                    b.Property<int>("IdentityTokenLifetime");

                    b.Property<bool>("IncludeJwtId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<Guid?>("LastModifierId");

                    b.Property<string>("LogoUri")
                        .HasMaxLength(2000);

                    b.Property<string>("PairWiseSubjectSalt")
                        .HasMaxLength(200);

                    b.Property<string>("ProtocolType")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("RefreshTokenExpiration");

                    b.Property<int>("RefreshTokenUsage");

                    b.Property<bool>("RequireClientSecret");

                    b.Property<bool>("RequireConsent");

                    b.Property<bool>("RequirePkce");

                    b.Property<int>("SlidingRefreshTokenLifetime");

                    b.Property<bool>("UpdateAccessTokenClaimsOnRefresh");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Clients","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientAggregate.ClientClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClientId");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<Guid?>("CreatorId");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<Guid?>("LastModifierId");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientClaims","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientAggregate.ClientSecret", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClientId");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("Expiration");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<Guid?>("LastModifierId");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientSecrets","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.EventLog.EventLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("ActivityId");

                    b.Property<string>("ApiName");

                    b.Property<string>("AuthenticationMethod");

                    b.Property<string>("Browser");

                    b.Property<string>("Category");

                    b.Property<string>("ClientId");

                    b.Property<string>("ClientName");

                    b.Property<bool>("ConsentRemembered");

                    b.Property<string>("Details");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Endpoint");

                    b.Property<string>("Error");

                    b.Property<string>("ErrorDescription");

                    b.Property<int>("EventId");

                    b.Property<int>("EventType");

                    b.Property<string>("GrantType");

                    b.Property<string>("Host");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LocalIpAddress");

                    b.Property<string>("Message");

                    b.Property<string>("Name");

                    b.Property<int>("ProcessId");

                    b.Property<string>("Provider");

                    b.Property<string>("ProviderUserId");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("RemoteIpAddress");

                    b.Property<string>("Scopes");

                    b.Property<string>("SubjectId");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("Token");

                    b.Property<string>("TokenType");

                    b.Property<string>("Url");

                    b.Property<string>("Username");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("EventLogs","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.IdentityResourceAggregate.IdentityResource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdentityResourceId");

                    b.Property<string>("ClaimTypes")
                        .HasMaxLength(2000);

                    b.Property<DateTime?>("CreationTime");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("DisplayName")
                        .HasMaxLength(200);

                    b.Property<bool>("Emphasize");

                    b.Property<bool>("Enabled");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<Guid?>("LastModifierId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("Required");

                    b.Property<bool>("ShowInDiscoveryDocument");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("IdentityResources","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.PersistedGrantAggregate.PersistedGrant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Data");

                    b.Property<DateTime?>("Expiration");

                    b.Property<string>("Key")
                        .HasMaxLength(200);

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .HasMaxLength(50);

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("PersistedGrants","ids");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiResourceAggregate.ApiResourceScope", b =>
                {
                    b.HasOne("KissU.Modules.IdentityServer.Domain.Models.ApiResourceAggregate.ApiResource", "ApiResource")
                        .WithMany("Scopes")
                        .HasForeignKey("ApiResourceId");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiResourceAggregate.ApiResourceSecret", b =>
                {
                    b.HasOne("KissU.Modules.IdentityServer.Domain.Models.ApiResourceAggregate.ApiResource", "ApiResource")
                        .WithMany("Secrets")
                        .HasForeignKey("ApiResourceId");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientAggregate.ClientClaim", b =>
                {
                    b.HasOne("KissU.Modules.IdentityServer.Domain.Models.ClientAggregate.Client", "Client")
                        .WithMany("Claims")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientAggregate.ClientSecret", b =>
                {
                    b.HasOne("KissU.Modules.IdentityServer.Domain.Models.ClientAggregate.Client", "Client")
                        .WithMany("ClientSecrets")
                        .HasForeignKey("ClientId");
                });
#pragma warning restore 612, 618
        }
    }
}
