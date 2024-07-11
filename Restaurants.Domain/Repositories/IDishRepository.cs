namespace Restaurants.Domain;

public interface IDishRepository
{
    Task<int> Create(Dish dish);
    Task Delete(IEnumerable<Dish> dishes);
}
