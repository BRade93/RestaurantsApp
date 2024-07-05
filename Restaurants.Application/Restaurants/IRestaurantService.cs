using Restaurants.Domain;

namespace Restaurants.Application;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetAllRestaurants();
    Task<Restaurant> GetRestaurantById(int id);
}
