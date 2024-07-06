using Restaurants.Application.Dtos;
using Restaurants.Domain;

namespace Restaurants.Application;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto> GetRestaurantById(int id);
    Task<int> Create(CreateRestaurantDto createRestaurantDto);
}
