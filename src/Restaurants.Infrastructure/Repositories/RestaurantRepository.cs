using System.Linq.Expressions;
using System.Net.Cache;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;
using Restaurants.Domain.Constants;

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
    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        var searchPhraseToLower = searchPhrase?.ToLower();

        var baseQuery = context
                .Restaurants
                .Where(r => searchPhraseToLower == null || (r.Name.ToLower().Contains(searchPhraseToLower)
                                                        || r.Description.ToLower().Contains(searchPhraseToLower)));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name), r => r.Name},
                {nameof(Restaurant.Description), r => r.Description},
                {nameof(Restaurant.Category), r => r.Category}
            };

            var selectedColumn = columnsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
        return (restaurants, totalCount);
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
