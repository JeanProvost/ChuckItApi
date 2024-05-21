using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChuckItApi.Models;

namespace ChuckItApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
            
        }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Image> Images { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Messages> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Listings)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);
            
              
        }
    }
}
