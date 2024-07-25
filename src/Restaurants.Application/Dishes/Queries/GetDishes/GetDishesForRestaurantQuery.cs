using MediatR;

namespace Restaurants.Application.Dishes.GetDishes;

public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; set; } = restaurantId;
}
