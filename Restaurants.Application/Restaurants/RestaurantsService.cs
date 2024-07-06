using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain;

namespace Restaurants.Application;

internal class RestaurantsService(IRestaurantRepository repository, ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantService
{
    public async Task<int> Create(CreateRestaurantDto createRestaurantDto)
    {
        logger.LogInformation("Creating restaurant.");
        var restaurant = mapper.Map<Restaurant>(createRestaurantDto);
        int id = await repository.Create(restaurant);

        return id;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
    }
    public async Task<RestaurantDto> GetRestaurantById(int id)
    {
        var restaurant = await repository.GetByIdAsync(id);
        if (restaurant == null)
        {
            throw new ArgumentException("Restaurant not found");
        }
        return mapper.Map<RestaurantDto>(restaurant);
    }

}
