﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KissU.Modules.IdentityServer.DbMigrator.Migrations
{
    [DbContext(typeof(DesignTimeDbContext))]
    internal class DesignTimeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiResource", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(1000)")
                    .HasMaxLength(1000);

                b.Property<string>("DisplayName")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<bool>("Enabled")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<byte[]>("Version")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.HasIndex("Name")
                    .IsUnique();

                b.ToTable("ApiResources", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiScope", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ApiResourceId")
                    .HasColumnType("int");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(1000)")
                    .HasMaxLength(1000);

                b.Property<string>("DisplayName")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<bool>("Emphasize")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<bool>("Required")
                    .HasColumnType("bit");

                b.Property<bool>("ShowInDiscoveryDocument")
                    .HasColumnType("bit");

                b.HasKey("Id");

                b.HasIndex("ApiResourceId");

                b.HasIndex("Name")
                    .IsUnique();

                b.ToTable("ApiScopes", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiSecret", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ApiResourceId")
                    .HasColumnType("int");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(1000)")
                    .HasMaxLength(1000);

                b.Property<DateTime?>("Expiration")
                    .HasColumnType("datetime2");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasColumnType("nvarchar(250)")
                    .HasMaxLength(250);

                b.Property<string>("Value")
                    .IsRequired()
                    .HasColumnType("nvarchar(4000)")
                    .HasMaxLength(4000);

                b.HasKey("Id");

                b.HasIndex("ApiResourceId");

                b.ToTable("ApiSecrets", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.Client", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("AbsoluteRefreshTokenLifetime")
                    .HasColumnType("int");

                b.Property<int>("AccessTokenLifetime")
                    .HasColumnType("int");

                b.Property<int>("AccessTokenType")
                    .HasColumnType("int");

                b.Property<bool>("AllowAccessTokensViaBrowser")
                    .HasColumnType("bit");

                b.Property<bool>("AllowOfflineAccess")
                    .HasColumnType("bit");

                b.Property<bool>("AllowPlainTextPkce")
                    .HasColumnType("bit");

                b.Property<bool>("AllowRememberConsent")
                    .HasColumnType("bit");

                b.Property<bool>("AlwaysIncludeUserClaimsInIdToken")
                    .HasColumnType("bit");

                b.Property<bool>("AlwaysSendClientClaims")
                    .HasColumnType("bit");

                b.Property<int>("AuthorizationCodeLifetime")
                    .HasColumnType("int");

                b.Property<bool>("BackChannelLogoutSessionRequired")
                    .HasColumnType("bit");

                b.Property<string>("BackChannelLogoutUri")
                    .HasColumnType("nvarchar(2000)")
                    .HasMaxLength(2000);

                b.Property<string>("ClientClaimsPrefix")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<string>("ClientId")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<string>("ClientName")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<string>("ClientUri")
                    .HasColumnType("nvarchar(2000)")
                    .HasMaxLength(2000);

                b.Property<int?>("ConsentLifetime")
                    .HasColumnType("int");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(1000)")
                    .HasMaxLength(1000);

                b.Property<bool>("EnableLocalLogin")
                    .HasColumnType("bit");

                b.Property<bool>("Enabled")
                    .HasColumnType("bit");

                b.Property<bool>("FrontChannelLogoutSessionRequired")
                    .HasColumnType("bit");

                b.Property<string>("FrontChannelLogoutUri")
                    .HasColumnType("nvarchar(2000)")
                    .HasMaxLength(2000);

                b.Property<int>("IdentityTokenLifetime")
                    .HasColumnType("int");

                b.Property<bool>("IncludeJwtId")
                    .HasColumnType("bit");

                b.Property<string>("LogoUri")
                    .HasColumnType("nvarchar(2000)")
                    .HasMaxLength(2000);

                b.Property<string>("PairWiseSubjectSalt")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<string>("ProtocolType")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<int>("RefreshTokenExpiration")
                    .HasColumnType("int");

                b.Property<int>("RefreshTokenUsage")
                    .HasColumnType("int");

                b.Property<bool>("RequireClientSecret")
                    .HasColumnType("bit");

                b.Property<bool>("RequireConsent")
                    .HasColumnType("bit");

                b.Property<bool>("RequirePkce")
                    .HasColumnType("bit");

                b.Property<int>("SlidingRefreshTokenLifetime")
                    .HasColumnType("int");

                b.Property<bool>("UpdateAccessTokenClaimsOnRefresh")
                    .HasColumnType("bit");

                b.Property<string>("UserCodeType")
                    .HasColumnType("nvarchar(100)")
                    .HasMaxLength(100);

                b.Property<byte[]>("Version")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.HasIndex("ClientId")
                    .IsUnique();

                b.ToTable("Clients", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientClaim", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ClientId")
                    .HasColumnType("int");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasColumnType("nvarchar(250)")
                    .HasMaxLength(250);

                b.Property<string>("Value")
                    .IsRequired()
                    .HasColumnType("nvarchar(250)")
                    .HasMaxLength(250);

                b.HasKey("Id");

                b.HasIndex("ClientId");

                b.ToTable("ClientClaims", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientSecret", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ClientId")
                    .HasColumnType("int");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(2000)")
                    .HasMaxLength(2000);

                b.Property<DateTime?>("Expiration")
                    .HasColumnType("datetime2");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasColumnType("nvarchar(250)")
                    .HasMaxLength(250);

                b.Property<string>("Value")
                    .IsRequired()
                    .HasColumnType("nvarchar(4000)")
                    .HasMaxLength(4000);

                b.HasKey("Id");

                b.HasIndex("ClientId");

                b.ToTable("ClientSecrets", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.DeviceFlowCode", b =>
            {
                b.Property<string>("UserCode")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<string>("ClientId")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime2");

                b.Property<string>("Data")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasMaxLength(50000);

                b.Property<string>("DeviceCode")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<DateTime?>("Expiration")
                    .IsRequired()
                    .HasColumnType("datetime2");

                b.Property<int>("Id")
                    .HasColumnType("int");

                b.Property<string>("SubjectId")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<byte[]>("Version")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("UserCode");

                b.HasIndex("DeviceCode")
                    .IsUnique();

                b.HasIndex("Expiration");

                b.ToTable("DeviceFlowCodes", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.IdentityResource", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(1000)")
                    .HasMaxLength(1000);

                b.Property<string>("DisplayName")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<bool>("Emphasize")
                    .HasColumnType("bit");

                b.Property<bool>("Enabled")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<bool>("Required")
                    .HasColumnType("bit");

                b.Property<bool>("ShowInDiscoveryDocument")
                    .HasColumnType("bit");

                b.Property<byte[]>("Version")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.HasIndex("Name")
                    .IsUnique();

                b.ToTable("IdentityResources", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.PersistedGrant", b =>
            {
                b.Property<string>("Key")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<string>("ClientId")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime2");

                b.Property<string>("Data")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)")
                    .HasMaxLength(50000);

                b.Property<DateTime?>("Expiration")
                    .HasColumnType("datetime2");

                b.Property<int>("Id")
                    .HasColumnType("int");

                b.Property<string>("SubjectId")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                b.Property<string>("Type")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);

                b.Property<byte[]>("Version")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Key");

                b.HasIndex("Expiration");

                b.HasIndex("SubjectId", "ClientId", "Type");

                b.ToTable("PersistedGrants", "ids");
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiResource", b =>
            {
                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ApiResourceClaim", "UserClaims", b1 =>
                {
                    b1.Property<int>("ApiResourceId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("Type")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b1.HasKey("ApiResourceId", "Id");

                    b1.ToTable("ApiClaims", "ids");

                    b1.WithOwner("ApiResource")
                        .HasForeignKey("ApiResourceId");
                });

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ApiResourceProperty", "Properties", b1 =>
                {
                    b1.Property<int>("ApiResourceId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b1.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b1.HasKey("ApiResourceId", "Id");

                    b1.ToTable("ApiProperties", "ids");

                    b1.WithOwner("ApiResource")
                        .HasForeignKey("ApiResourceId");
                });
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiScope", b =>
            {
                b.HasOne("KissU.Modules.IdentityServer.Domain.Models.ApiResource", "ApiResource")
                    .WithMany("Scopes")
                    .HasForeignKey("ApiResourceId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ApiScopeClaim", "UserClaims", b1 =>
                {
                    b1.Property<int>("ApiScopeId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b1.HasKey("ApiScopeId", "Id");

                    b1.ToTable("ApiScopeClaims", "ids");

                    b1.WithOwner("ApiScope")
                        .HasForeignKey("ApiScopeId");
                });
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ApiSecret", b =>
            {
                b.HasOne("KissU.Modules.IdentityServer.Domain.Models.ApiResource", "ApiResource")
                    .WithMany("ApiSecrets")
                    .HasForeignKey("ApiResourceId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.Client", b =>
            {
                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ClientCorsOrigin", "AllowedCorsOrigins", b1 =>
                {
                    b1.Property<int>("ClientId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b1.HasKey("ClientId", "Id");

                    b1.ToTable("ClientCorsOrigins", "ids");

                    b1.WithOwner("Client")
                        .HasForeignKey("ClientId");
                });

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ClientGrantType", "AllowedGrantTypes", b1 =>
                {
                    b1.Property<int>("ClientId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("GrantType")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b1.HasKey("ClientId", "Id");

                    b1.ToTable("ClientGrantTypes", "ids");

                    b1.WithOwner("Client")
                        .HasForeignKey("ClientId");
                });

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ClientIdPRestriction",
                    "IdentityProviderRestrictions", b1 =>
                    {
                        b1.Property<int>("ClientId")
                            .HasColumnType("int");

                        b1.Property<int>("Id")
                            .ValueGeneratedOnAdd()
                            .HasColumnType("int")
                            .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn);

                        b1.Property<string>("Provider")
                            .IsRequired()
                            .HasColumnType("nvarchar(200)")
                            .HasMaxLength(200);

                        b1.HasKey("ClientId", "Id");

                        b1.ToTable("ClientIdPRestrictions", "ids");

                        b1.WithOwner("Client")
                            .HasForeignKey("ClientId");
                    });

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ClientPostLogoutRedirectUri",
                    "PostLogoutRedirectUris", b1 =>
                    {
                        b1.Property<int>("ClientId")
                            .HasColumnType("int");

                        b1.Property<int>("Id")
                            .ValueGeneratedOnAdd()
                            .HasColumnType("int")
                            .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                SqlServerValueGenerationStrategy.IdentityColumn);

                        b1.Property<string>("PostLogoutRedirectUri")
                            .IsRequired()
                            .HasColumnType("nvarchar(2000)")
                            .HasMaxLength(2000);

                        b1.HasKey("ClientId", "Id");

                        b1.ToTable("ClientPostLogoutRedirectUris", "ids");

                        b1.WithOwner("Client")
                            .HasForeignKey("ClientId");
                    });

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ClientProperty", "Properties", b1 =>
                {
                    b1.Property<int>("ClientId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b1.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b1.HasKey("ClientId", "Id");

                    b1.ToTable("ClientPropertys", "ids");

                    b1.WithOwner("Client")
                        .HasForeignKey("ClientId");
                });

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ClientRedirectUri", "RedirectUris", b1 =>
                {
                    b1.Property<int>("ClientId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("RedirectUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b1.HasKey("ClientId", "Id");

                    b1.ToTable("ClientRedirectUris", "ids");

                    b1.WithOwner("Client")
                        .HasForeignKey("ClientId");
                });

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.ClientScope", "AllowedScopes", b1 =>
                {
                    b1.Property<int>("ClientId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("Scope")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b1.HasKey("ClientId", "Id");

                    b1.ToTable("ClientScopes", "ids");

                    b1.WithOwner("Client")
                        .HasForeignKey("ClientId");
                });
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientClaim", b =>
            {
                b.HasOne("KissU.Modules.IdentityServer.Domain.Models.Client", "Client")
                    .WithMany("Claims")
                    .HasForeignKey("ClientId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.ClientSecret", b =>
            {
                b.HasOne("KissU.Modules.IdentityServer.Domain.Models.Client", "Client")
                    .WithMany("ClientSecrets")
                    .HasForeignKey("ClientId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("KissU.Modules.IdentityServer.Domain.Models.IdentityResource", b =>
            {
                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.IdentityResourceClaim", "UserClaims", b1 =>
                {
                    b1.Property<int>("IdentityResourceId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b1.HasKey("IdentityResourceId", "Id");

                    b1.ToTable("IdentityClaims", "ids");

                    b1.WithOwner("IdentityResource")
                        .HasForeignKey("IdentityResourceId");
                });

                b.OwnsMany("KissU.Modules.IdentityServer.Domain.Models.IdentityResourceProperty", "Properties", b1 =>
                {
                    b1.Property<int>("IdentityResourceId")
                        .HasColumnType("int");

                    b1.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b1.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b1.HasKey("IdentityResourceId", "Id");

                    b1.ToTable("IdentityProperties", "ids");

                    b1.WithOwner("IdentityResource")
                        .HasForeignKey("IdentityResourceId");
                });
            });
#pragma warning restore 612, 618
        }
    }
}