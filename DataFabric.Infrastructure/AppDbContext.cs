using DataFabric.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataFabric.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 🔐 Auth
        public DbSet<User> Users { get; set; }

        // 🧩 Dynamic Data
        public DbSet<EntityField> EntityFields { get; set; }
        public DbSet<EntityLink> EntityLinks { get; set; }
        public DbSet<EntityRecord> EntityRecords { get; set; }
        public DbSet<EntitySearch> EntitySearches { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // 🔐 USER CONFIG
            // =========================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(x => x.Password)
                      .IsRequired();

                entity.Property(x => x.RefreshToken)
                      .HasMaxLength(255);

                entity.Property(x => x.RefreshTokenExpiryTime);

                entity.Property(x => x.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasIndex(x => x.Email).IsUnique();

                entity.HasIndex(x => x.Username).IsUnique();
            });

            // =========================
            // 🧩 ENTITY TYPE
            // =========================
            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // =========================
            // 🧩 ENTITY RECORD
            // =========================
            modelBuilder.Entity<EntityRecord>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne<EntityType>()
                      .WithMany()
                      .HasForeignKey(x => x.EntityTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.Data)
                      .HasColumnType("nvarchar(max)");

                entity.HasIndex(x => x.Name);
            });

            // =========================
            // 🧩 ENTITY FIELD 
            // =========================
            modelBuilder.Entity<EntityField>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(x => x.DataType)
                      .IsRequired();

                entity.HasOne<EntityType>()
                      .WithMany(r => r.Fields)
                      .HasForeignKey("EntityTypeId");

                // 🔥 IMPORTANT INDEX (for your search performance)
                entity.HasIndex(x => new { x.Name, x.EntityTypeId });

                entity.HasIndex("EntityTypeId");
            });

            modelBuilder.Entity<EntitySearch>(entity =>
            {
                entity.HasKey(e => new { e.EntityRecordId, e.FieldName });

                entity.Property(x => x.FieldName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(x => x.Value)
                      .HasMaxLength(2000);

                entity.HasIndex(x => new { x.FieldName, x.Value });
                entity.HasIndex(x => x.EntityRecordId);

                entity.HasOne<EntityRecord>()
                      .WithMany()
                      .HasForeignKey(x => x.EntityRecordId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EntityLink>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(x => x.SourceEntityId);
                entity.HasIndex(x => x.TargetEntityId);
            });
        }
    }
}