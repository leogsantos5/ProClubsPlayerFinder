﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProClubsPlayerFinder.API.Data;

#nullable disable

namespace ProClubsPlayerFinder.API.Migrations
{
    [DbContext(typeof(ClubsPlayerFinderEafc24Context))]
    partial class ClubsPlayerFinderEafc24ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "45ea90a9-22b4-40b3-9362-2eacc75a45bc",
                            Name = "Free Agent",
                            NormalizedName = "FREE AGENT"
                        },
                        new
                        {
                            Id = "19c543d9-cf25-41b2-a1d0-0838dfc0c2ec",
                            Name = "Player",
                            NormalizedName = "PLAYER"
                        },
                        new
                        {
                            Id = "77ea7228-bcbd-4b43-8c7f-77db97f3c168",
                            Name = "Club Owner",
                            NormalizedName = "CLUB OWNER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                            RoleId = "45ea90a9-22b4-40b3-9362-2eacc75a45bc"
                        },
                        new
                        {
                            UserId = "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                            RoleId = "45ea90a9-22b4-40b3-9362-2eacc75a45bc"
                        },
                        new
                        {
                            UserId = "71a8b6ac-6395-41d8-94d4-e103350110c0",
                            RoleId = "45ea90a9-22b4-40b3-9362-2eacc75a45bc"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProClubsPlayerFinder.API.Data.ApiUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("ClubId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Console")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Country")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GamingPlatformAccountId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id")
                        .HasName("PK__tmp_ms_x__4A4E74C8A7D90612");

                    b.HasIndex("ClubId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "71a8b6ac-6395-41d8-94d4-e103350110c0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0f1a47ff-aadc-4960-8fdc-0284d6df09b9",
                            Console = "PS5",
                            Country = "Portugal",
                            DateOfBirth = new DateTime(2001, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "leogsantos5@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Leonardo",
                            GamingPlatformAccountId = "leosantos2001",
                            LastName = "Santos",
                            LockoutEnabled = false,
                            NormalizedEmail = "LEOGSANTOS5@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEKx1xPVACj3XawLXuVmwfh7qx/yu4rX8ljI7rKiYzXgWzCdDIJL7JiPY5lJc0/qjOQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "63375f83-86dd-4032-8f77-e5aa0d984353",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3a0e88bd-842f-4f16-99a2-4ff8ce726e70",
                            Console = "PS5",
                            Country = "Portugal",
                            DateOfBirth = new DateTime(2000, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ms2626242@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Marcelo",
                            GamingPlatformAccountId = "Marcelocool181",
                            LastName = "Silva",
                            LockoutEnabled = false,
                            NormalizedEmail = "MS2626242@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAED8cc4kBG4TyI8y5BcAtbL75Unsdg5l/P4fYkasM/30RTLcUHYIJrRDDHLL3XwTXRg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "17e95833-d5ac-4a48-ac36-f1599a3db5ae",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "eb8611d0-89a4-43f2-9a8a-2d2f12dc00fc",
                            Console = "PS5",
                            Country = "Portugal",
                            DateOfBirth = new DateTime(2003, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "bernastheking@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Bernardo",
                            GamingPlatformAccountId = "Bernardo_S_14",
                            LastName = "Santos",
                            LockoutEnabled = false,
                            NormalizedEmail = "BERNASTHEKING@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEPErjwNr8OIKHtid7DNn+OCPSrCwJnSw9vJw1omvnUYtfcPsO7F/i/IY/MV2mW6+PA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "851d1cc2-ed7a-4332-93ee-08637fed06a9",
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("ProClubsPlayerFinder.API.Data.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClubName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Console")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("OwnerPlayerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id")
                        .HasName("PK__Clubs__D35058E7947E6D35");

                    b.HasIndex("OwnerPlayerId")
                        .IsUnique();

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProClubsPlayerFinder.API.Data.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProClubsPlayerFinder.API.Data.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProClubsPlayerFinder.API.Data.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ProClubsPlayerFinder.API.Data.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProClubsPlayerFinder.API.Data.ApiUser", b =>
                {
                    b.HasOne("ProClubsPlayerFinder.API.Data.Club", "Club")
                        .WithMany("Players")
                        .HasForeignKey("ClubId")
                        .HasConstraintName("FK_Players_Clubs");

                    b.Navigation("Club");
                });

            modelBuilder.Entity("ProClubsPlayerFinder.API.Data.Club", b =>
                {
                    b.HasOne("ProClubsPlayerFinder.API.Data.ApiUser", "OwnerPlayer")
                        .WithOne()
                        .HasForeignKey("ProClubsPlayerFinder.API.Data.Club", "OwnerPlayerId")
                        .IsRequired()
                        .HasConstraintName("FK__Clubs__OwnerPlay__52593CB8");

                    b.Navigation("OwnerPlayer");
                });

            modelBuilder.Entity("ProClubsPlayerFinder.API.Data.Club", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
