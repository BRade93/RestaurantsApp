using Restaurants.Domain;

namespace Restaurants.Infrastructure;

internal class DishRepository(RestaurantDbContext context) : IDishRepository
{
    public async Task<int> Create(Dish dish)
    {
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();

        return dish.Id;
    }

    public async Task Delete(IEnumerable<Dish> dishes)
    {
        context.Dishes.RemoveRange(dishes);
        await context.SaveChangesAsync();
    }
}
