using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ticketverkoop.Domain.Entities
{
    public partial class VoetbalContext : DbContext
    {
        public VoetbalContext()
        {
        }

        public VoetbalContext(DbContextOptions<VoetbalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abonnement> Abonnement { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Club> Club { get; set; }
        public virtual DbSet<Ring> Ring { get; set; }
        public virtual DbSet<Seizoen> Seizoen { get; set; }
        public virtual DbSet<Stadion> Stadion { get; set; }
        public virtual DbSet<StadionRingVak> StadionRingVak { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<Vak> Vak { get; set; }
        public virtual DbSet<Wedstrijd> Wedstrijd { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server = voetbalavtjs.database.windows.net;  Initial Catalog = VoetbalSQL; ; User ID = voetbalavtjs; Password = azerty-123; MultipleActiveResultSets = True; Encrypt = True; TrustServerCertificate = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Abonnement>(entity =>
            {
                entity.Property(e => e.AbonnementId).HasColumnName("Abonnement_ID");

                entity.Property(e => e.ClubId).HasColumnName("Club_ID");

                entity.Property(e => e.SeizoenId).HasColumnName("Seizoen_ID");


                entity.Property(e => e.ZitplaatsNr).HasColumnName("Zitplaats_Nr");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Abonnement)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonnement_Club");

                entity.HasOne(d => d.Seizoen)
                    .WithMany(p => p.Abonnement)
                    .HasForeignKey(d => d.SeizoenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonnement_Seizoen");

            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.Property(e => e.ClubId)
                    .HasColumnName("Club_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StadionId).HasColumnName("Stadion_ID");

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.Club)
                    .HasForeignKey(d => d.StadionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Club_Stadion");
            });

            
            modelBuilder.Entity<Ring>(entity =>
            {
                entity.Property(e => e.RingId)
                    .HasColumnName("Ring_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Seizoen>(entity =>
            {
                entity.Property(e => e.SeizoenId)
                    .HasColumnName("Seizoen_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Einddatum).HasColumnType("date");

                entity.Property(e => e.Startdatum).HasColumnType("date");
            });

            modelBuilder.Entity<Stadion>(entity =>
            {
                entity.Property(e => e.StadionId)
                    .HasColumnName("Stadion_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StadionRingVak>(entity =>
            {
                entity.ToTable("Stadion_Ring_Vak");

                entity.Property(e => e.StadionRingVakId)
                    .HasColumnName("Stadion_Ring_Vak_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Prijs).HasColumnType("money");

                entity.Property(e => e.RingId).HasColumnName("Ring_ID");

                entity.Property(e => e.StadionId).HasColumnName("Stadion_ID");

                entity.Property(e => e.VakId).HasColumnName("Vak_ID");

                entity.HasOne(d => d.Ring)
                    .WithMany(p => p.StadionRingVak)
                    .HasForeignKey(d => d.RingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stadion_Ring_Vak_Ring");

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.StadionRingVak)
                    .HasForeignKey(d => d.StadionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stadion_Ring_Vak_Stadion");

                entity.HasOne(d => d.Vak)
                    .WithMany(p => p.StadionRingVak)
                    .HasForeignKey(d => d.VakId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stadion_Ring_Vak_Vak");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");


                entity.Property(e => e.WedstrijdId).HasColumnName("Wedstrijd_ID");

                entity.Property(e => e.ZitplaatsNr).HasColumnName("Zitplaats_Nr");


                entity.HasOne(d => d.Wedstrijd)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.WedstrijdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Wedstrijd");
            });

            modelBuilder.Entity<Vak>(entity =>
            {
                entity.Property(e => e.VakId)
                    .HasColumnName("Vak_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Wedstrijd>(entity =>
            {
                entity.Property(e => e.WedstrijdId)
                    .HasColumnName("Wedstrijd_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.StadionId).HasColumnName("Stadion_ID");

                entity.Property(e => e.ThuisploegId).HasColumnName("Thuisploeg_ID");

                entity.Property(e => e.UitploegId).HasColumnName("Uitploeg_ID");

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.Wedstrijd)
                    .HasForeignKey(d => d.StadionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wedstrijd_Stadion");

                entity.HasOne(d => d.Thuisploeg)
                    .WithMany(p => p.WedstrijdThuisploeg)
                    .HasForeignKey(d => d.ThuisploegId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wedstrijd_Club");

                entity.HasOne(d => d.Uitploeg)
                    .WithMany(p => p.WedstrijdUitploeg)
                    .HasForeignKey(d => d.UitploegId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wedstrijd_Club1");
            });
        }
    }
}
