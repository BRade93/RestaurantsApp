using System.Net.Cache;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure;

internal class RestaurantRepository(RestaurantDbContext context) : IRestaurantRepository
{
    public async Task<int> Create(Restaurant restaurant)
    {
        await context.AddAsync(restaurant);
        await context.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant restaurant)
    {
        context.Remove(restaurant);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await context.Restaurants.Include(dish => dish.Dishes).ToListAsync();
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        return await context.Restaurants.Include(dish => dish.Dishes).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task Update()
    {
        await context.SaveChangesAsync();
    }
}
