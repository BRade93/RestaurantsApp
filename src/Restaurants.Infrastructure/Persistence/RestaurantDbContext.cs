using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure;

internal class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("YourConnectionString", sqlOptions =>
            {
                // Enable retry on failure for transient errors
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5, // Number of retries
                    maxRetryDelay: TimeSpan.FromSeconds(30), // Max delay between retries
                    errorNumbersToAdd: null // Retry on all transient errors
                );
            });
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);

        modelBuilder.Entity<User>()
            .HasMany(r => r.OwnedRestaurants)
            .WithOne(o => o.Owner)
            .HasForeignKey(f => f.OwnerId);
        modelBuilder.Entity<Restaurant>()
            .HasOne(r => r.Owner)  // Assuming Owner is a navigation property
            .WithMany(u => u.OwnedRestaurants)
            .HasForeignKey(r => r.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
