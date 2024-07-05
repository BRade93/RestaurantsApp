using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application;

internal class RestaurantsService(IRestaurantRepository repository, ILogger<RestaurantsService> logger) : IRestaurantService
{
    public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        return await repository.GetAllAsync();
    }

    public Task<Restaurant> GetRestaurantById(int id)
    {
        return repository.GetByIdAsync(id);
    }
}
