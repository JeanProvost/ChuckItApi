using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChuckItApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using ChuckItApi.Data.Seeds;

namespace ChuckItApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(150000);
        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Listings)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Listing>()
                .HasMany(l => l.Images)
                .WithOne(i => i.Listing)
                .HasForeignKey(i => i.ListingId);

            modelBuilder.Entity<Location>()
                .Property(l => l.LocationId)
                .ValueGeneratedOnAdd();
        }
    }
}
