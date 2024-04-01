using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.ClassLibrary;

namespace ProClubsPlayerFinder.API.Data;

public partial class ClubsPlayerFinderEafc24Context : IdentityDbContext<ApiUser>
{
    public ClubsPlayerFinderEafc24Context()
    {
    }

    public ClubsPlayerFinderEafc24Context(DbContextOptions<ClubsPlayerFinderEafc24Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<ApiUser> Players { get; set; }

    public virtual DbSet<Invite> Invites { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=ClubsPlayerFinderEAFC24;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Invite>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ApiUserId).IsRequired();
            entity.Property(e => e.ClubId).IsRequired();
            //entity.Property(e => e.Message).HasMaxLength(256);
            //entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue(Status.Pending);

            entity.HasOne(e => e.ApiUser)
                .WithOne()
                .HasForeignKey<Invite>(e => e.ApiUserId)
                .HasConstraintName("FK_Invites_Players_Receiver")
                .OnDelete(DeleteBehavior.Cascade); // You may adjust the delete behavior based on your requirements

            entity.HasOne(e => e.Club)
                .WithOne()
                .HasForeignKey<Invite>(e => e.ClubId)
                .HasConstraintName("FK_Invites_Clubs_Sender")
                .OnDelete(DeleteBehavior.Cascade); // You may adjust the delete behavior based on your requirements
        });
        
        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ApiUserId).IsRequired();
            entity.Property(e => e.ClubId).IsRequired();
            //entity.Property(e => e.Message).HasMaxLength(256);
            //entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue(Status.Pending);

            entity.HasOne(e => e.ApiUser)
                .WithOne()
                .HasForeignKey<Request>(e => e.ApiUserId)
                .HasConstraintName("FK_Requests_Players_Sender")
                .OnDelete(DeleteBehavior.Cascade); // You may adjust the delete behavior based on your requirements

            entity.HasOne(e => e.Club)
                .WithOne()
                .HasForeignKey<Request>(e => e.ClubId)
                .HasConstraintName("FK_Requests_Clubs_Receiver")
                .OnDelete(DeleteBehavior.Cascade); // You may adjust the delete behavior based on your requirements
        });


        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clubs__D35058E7947E6D35");

            //entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClubName).HasMaxLength(50);
            entity.Property(e => e.Console).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(512);

            entity.HasOne(d => d.OwnerPlayer).WithOne()
                .HasForeignKey<Club>(d => d.OwnerPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clubs__OwnerPlay__52593CB8");
        });

        modelBuilder.Entity<ApiUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__4A4E74C8A7D90612");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(512);
            entity.Property(e => e.Console).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.GamingPlatformAccountId).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.Club).WithMany(p => p.Players)
                .HasForeignKey(d => d.ClubId)
                .HasConstraintName("FK_Players_Clubs");
        });

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "Free Agent",
                NormalizedName = "FREE AGENT",
                Id = "45ea90a9-22b4-40b3-9362-2eacc75a45bc"
            },
            new IdentityRole
            {
                Name = "Player",
                NormalizedName = "PLAYER",
                Id = "19c543d9-cf25-41b2-a1d0-0838dfc0c2ec"
            },
            new IdentityRole
            {
                Name = "Club Owner",
                NormalizedName = "CLUB OWNER",
                Id = "77ea7228-bcbd-4b43-8c7f-77db97f3c168"
            }
        );

        var hasher = new PasswordHasher<ApiUser>();

        modelBuilder.Entity<ApiUser>().HasData(
        new ApiUser
        {
            Id = "71a8b6ac-6395-41d8-94d4-e103350110c0",
            Email = "leogsantos5@gmail.com",
            NormalizedEmail = "LEOGSANTOS5@GMAIL.COM",
            FirstName = "Leonardo",
            LastName = "Santos",
            GamingPlatformAccountId = "leosantos2001",
            Country = "Portugal",
            DateOfBirth = new DateTime(2001, 07, 13),
            Console = "PS5",
            PasswordHash = hasher.HashPassword(null, "Pterodactil123")
        },
        new ApiUser
        {
            Id = "7b867b03-fd4a-4410-a9fd-abdc32bda959",
            Email = "ms2626242@gmail.com",
            NormalizedEmail = "MS2626242@GMAIL.COM",
            FirstName = "Marcelo",
            LastName = "Silva",
            GamingPlatformAccountId = "Marcelocool181",
            Country = "Portugal",
            DateOfBirth = new DateTime(2000, 11, 28),
            Console = "PS5",
            PasswordHash = hasher.HashPassword(null, "Ressaltos4Life")
        },
        new ApiUser
        {
            Id = "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
            Email = "bernastheking@gmail.com",
            NormalizedEmail = "BERNASTHEKING@GMAIL.COM",
            FirstName = "Bernardo",
            LastName = "Santos",
            GamingPlatformAccountId = "Bernardo_S_14",
            Country = "Portugal",
            DateOfBirth = new DateTime(2003, 07, 11),
            Console = "PS5",
            PasswordHash = hasher.HashPassword(null, "EsquerdaArder")
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "45ea90a9-22b4-40b3-9362-2eacc75a45bc", // All free agents for now
                UserId = "2ec68e2f-fa24-4abf-8bdf-d82f530adee0"
            },
            new IdentityUserRole<string>
            {
                RoleId = "45ea90a9-22b4-40b3-9362-2eacc75a45bc",
                UserId = "7b867b03-fd4a-4410-a9fd-abdc32bda959"
            },
            new IdentityUserRole<string>
            {
                RoleId = "45ea90a9-22b4-40b3-9362-2eacc75a45bc",
                UserId = "71a8b6ac-6395-41d8-94d4-e103350110c0"
            });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
