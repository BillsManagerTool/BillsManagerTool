using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class BillsManager_DevContext : DbContext
    {
        public BillsManager_DevContext()
        {
        }

        public BillsManager_DevContext(DbContextOptions<BillsManager_DevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Charge> Charges { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<CostType> CostTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Entrance> Entrances { get; set; }
        public virtual DbSet<NotificationSetting> NotificationSettings { get; set; }
        public virtual DbSet<Occupant> Occupants { get; set; }
        public virtual DbSet<OccupantDetail> OccupantDetails { get; set; }
        public virtual DbSet<OccupantToApartment> OccupantToApartments { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Town> Towns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=BillsManager_Dev;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("Apartment");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.CostCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostCenterApartment");

                entity.HasOne(d => d.Entrance)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.EntranceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntranceApartment");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.TownId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TownBuilding");
            });

            modelBuilder.Entity<Charge>(entity =>
            {
                entity.ToTable("Charge");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DueAmount).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.IsPaid).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.Charges)
                    .HasForeignKey(d => d.CostCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostCenterCharge");

                entity.HasOne(d => d.CostType)
                    .WithMany(p => p.Charges)
                    .HasForeignKey(d => d.CostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostTypeCharge");
            });

            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.ToTable("CostCenter");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.TotalDueAmount).HasColumnType("decimal(13, 2)");

                entity.HasOne(d => d.Occupant)
                    .WithMany(p => p.CostCenters)
                    .HasForeignKey(d => d.OccupantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OccupantCostCenter");
            });

            modelBuilder.Entity<CostType>(entity =>
            {
                entity.ToTable("CostType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.AlphaCode3)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Entrance>(entity =>
            {
                entity.ToTable("Entrance");

                entity.Property(e => e.EntranceNumber)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Entrances)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuildingEntrance");
            });

            modelBuilder.Entity<NotificationSetting>(entity =>
            {
                entity.HasKey(e => e.SettingsKey)
                    .HasName("PK__Notifica__BA44B3F78D873FFD");

                entity.ToTable("NotificationSettings", "Settings");

                entity.Property(e => e.BusinessEmail)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<Occupant>(entity =>
            {
                entity.ToTable("Occupant");

                entity.Property(e => e.LeaveDate).HasColumnType("date");

                entity.Property(e => e.PeriodStart).HasColumnType("date");

                entity.HasOne(d => d.OccupantDetails)
                    .WithMany(p => p.Occupants)
                    .HasForeignKey(d => d.OccupantDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OccupantDetailsOccupant");
            });

            modelBuilder.Entity<OccupantDetail>(entity =>
            {
                entity.HasKey(e => e.OccupantDetailsId)
                    .HasName("PK__Occupant__C28410EBF8EE1396");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.FirstName).HasMaxLength(32);

                entity.Property(e => e.IsCurrentOccupant).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsHousekeeper).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOwner).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(32);

                entity.Property(e => e.MobileNumber).HasMaxLength(32);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<OccupantToApartment>(entity =>
            {
                entity.ToTable("OccupantToApartment");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.OccupantToApartments)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Apartment_OccupantToApartment");

                entity.HasOne(d => d.Occupant)
                    .WithMany(p => p.OccupantToApartments)
                    .HasForeignKey(d => d.OccupantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Occupant_OccupantToApartment");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken", "Security");

                entity.Property(e => e.Created).HasColumnType("smalldatetime");

                entity.Property(e => e.Expires).HasColumnType("smalldatetime");

                entity.Property(e => e.ReasonRevoked).HasMaxLength(64);

                entity.Property(e => e.ReplacedByToken).HasMaxLength(1024);

                entity.Property(e => e.Revoked).HasColumnType("smalldatetime");

                entity.Property(e => e.RevokedByIp).HasMaxLength(32);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.HasOne(d => d.OccupantDetails)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.OccupantDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OccupanтDetailsRefreshToken");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.ToTable("Town");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryTown");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
