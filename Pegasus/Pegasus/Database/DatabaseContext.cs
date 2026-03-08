using Microsoft.EntityFrameworkCore;
using Pegasus.Database.Model;
using System;

namespace Pegasus.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Friend> Friend { get; set; }
        public DbSet<Dungeon> Dungeon { get; set; }
        public DbSet<DungeonTile> DungeonTile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string host = Environment.GetEnvironmentVariable("PEGASUS_DB_HOSTNAME") ?? "postgres";
                string port = Environment.GetEnvironmentVariable("PEGASUS_DB_PORT") ?? "5432";
                string database = Environment.GetEnvironmentVariable("PEGASUS_DB_DATABASE") ?? "pegasus";
                string username = Environment.GetEnvironmentVariable("PEGASUS_DB_USERNAME") ?? "pegasus";
                string password = Environment.GetEnvironmentVariable("PEGASUS_DB_PASSWORD") ?? "somelongasspassword";

                string connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
                
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Friend relationships
            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.FriendIdNavigation)
                    .HasForeignKey<Friend>(d => d.Id);

                entity.HasOne(d => d.Friend1Navigation)
                    .WithMany(p => p.FriendFriend1Navigation)
                    .HasForeignKey(d => d.Friend1);
            });

            // Configure Dungeon entity
            modelBuilder.Entity<Dungeon>(entity =>
            {
                entity.HasKey(e => e.LandBlockId);

                entity.HasMany(d => d.DungeonTile)
                    .WithOne(p => p.LandBlock)
                    .HasForeignKey(d => d.LandBlockId);
            });

            // Configure DungeonTile entity
            modelBuilder.Entity<DungeonTile>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
} 