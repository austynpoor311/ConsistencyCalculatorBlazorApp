using System;
using System.Collections.Generic;
using ConsistencyCalculator.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConsistencyCalculator.Data.DbContexts
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Player> Players { get; private set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Injury> Injuries { get; set; }
        public virtual DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Team>().ToTable("Teams", "dbo");
            modelBuilder.Entity<Player>().ToTable("Players", "dbo");
            modelBuilder.Entity<Injury>().ToTable("Injuries", "dbo");
            modelBuilder.Entity<Game>().ToTable("Games", "dbo");
            modelBuilder.Entity<Position>()
                .HasMany(po => po.Players)
                .WithOne(p => p.Position);
        }
    }
}
