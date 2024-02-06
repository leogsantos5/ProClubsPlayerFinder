using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProClubsPlayerFinder.API.Data;

public partial class ClubsPlayerFinderEafc24Context : DbContext
{
    public ClubsPlayerFinderEafc24Context()
    {
    }

    public ClubsPlayerFinderEafc24Context(DbContextOptions<ClubsPlayerFinderEafc24Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=ClubsPlayerFinderEAFC24;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PK__Clubs__D35058E7947E6D35");

            entity.Property(e => e.ClubId).ValueGeneratedNever();
            entity.Property(e => e.ClubName).HasMaxLength(50);
            entity.Property(e => e.Console).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(512);

            entity.HasOne(d => d.OwnerPlayer).WithOne()
                .HasForeignKey<Club>(d => d.OwnerPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clubs__OwnerPlay__52593CB8");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__tmp_ms_x__4A4E74C8A7D90612");

            entity.Property(e => e.PlayerId).ValueGeneratedNever();
            entity.Property(e => e.Console).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.GamingPlatformAccountId).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.Club).WithMany(p => p.Players)
                .HasForeignKey(d => d.ClubId)
                .HasConstraintName("FK_Players_Clubs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
