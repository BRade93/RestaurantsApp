using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure;

internal class RestaurantRepository(RestaurantDbContext context) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await context.Restaurants.ToListAsync();
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        return await context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
    }
}
