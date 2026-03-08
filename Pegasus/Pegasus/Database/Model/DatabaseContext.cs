using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pegasus.Configuration;

namespace Pegasus.Database.Model
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Dungeon> Dungeon { get; set; }
        public virtual DbSet<DungeonTile> DungeonTile { get; set; }
        public virtual DbSet<Friend> Friend { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMySql($"server={ConfigManager.Config.MySql.Host};port={ConfigManager.Config.MySql.Port};user={ConfigManager.Config.MySql.Username}" +
                //    $";password={ConfigManager.Config.MySql.Password};database={ConfigManager.Config.MySql.Database}");
                string dbHostname = Environment.GetEnvironmentVariable("PEGASUS_DB_HOSTNAME");
                string dbPort = Environment.GetEnvironmentVariable("PEGASUS_DB_PORT");
                string dbUsername = Environment.GetEnvironmentVariable("PEGASUS_DB_USERNAME");
                string dbPassword = Environment.GetEnvironmentVariable("PEGASUS_DB_PASSWORD");
                string dbDatabase = Environment.GetEnvironmentVariable("PEGASUS_DB_DATABASE");
                string dbSSLMode = Environment.GetEnvironmentVariable("PEGASUS_DB_SSL_MODE");
                string dbTrustServerCert = Environment.GetEnvironmentVariable("PEGASUS_DB_TRUST_SERVER_CERT");

                optionsBuilder.UseNpgsql($"server={dbHostname};port={dbPort};username={dbUsername};password={dbPassword};database={dbDatabase};SSL Mode={dbSSLMode};Trust Server Certificate={dbTrustServerCert}");
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id).HasColumnName("id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreateIp)
                    .IsRequired()
                    .HasColumnName("createip")
                    .HasColumnType("character varying(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createtime")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("'now()'");

                entity.Property(e => e.LastIp)
                    .IsRequired()
                    .HasColumnName("lastip")
                    .HasColumnType("character varying(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LastTime)
                    .HasColumnName("lasttime")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("'now()'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("character varying(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Privileges)
                    .HasColumnName("privileges")
                    .HasColumnType("smallint")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("character varying(50)")
                    .HasDefaultValueSql("''");

                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Dungeon>(entity =>
            {
                entity.HasKey(e => e.LandBlockId)
                    .HasName("pk_dungeon_id");

                entity.ToTable("dungeon");

                entity.Property(e => e.LandBlockId)
                    .HasColumnType("bigint")
                    .HasColumnName("landblockid")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying(50)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<DungeonTile>(entity =>
            {
                entity.ToTable("dungeon_tile");

                entity.HasIndex(e => e.LandBlockId)
                    .HasName("fk_landblockid_landblockid_dungeon");

                entity.Property(e => e.Id).HasColumnName("id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LandBlockId)
                    .HasColumnType("bigint")
                    .HasColumnName("landblockid")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TileId)
                    .HasColumnName("tileid")
                    .HasColumnType("bigint")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.X)
                    .HasColumnName("x")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Y)
                    .HasColumnName("y")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Z)
                    .HasColumnName("z")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.LandBlock)
                    .WithMany(p => p.DungeonTile)
                    .HasForeignKey(d => d.LandBlockId)
                    .HasConstraintName("fk_landblockid_dungeon");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.ToTable("friend");

                entity.HasIndex(e => e.Friend1)
                    .HasName("fk_friend_friend_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AddTime)
                    .HasColumnName("addtime")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("'now()'");

                entity.Property(e => e.Friend1)
                    .HasColumnName("friend")
                    .HasColumnType("bigint")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Friend1Navigation)
                    .WithMany(p => p.FriendFriend1Navigation)
                    .HasForeignKey(d => d.Friend1)
                    .HasConstraintName("fk_friend_account");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.FriendIdNavigation)
                    .HasForeignKey<Friend>(d => d.Id)
                    .HasConstraintName("fk_friend_id");
            });
        }
    }
}
