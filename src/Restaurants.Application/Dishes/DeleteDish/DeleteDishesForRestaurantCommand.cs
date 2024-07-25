using MediatR;

namespace Restaurants.Application.Dishes.DeleteDish;

public class DeleteDishesForRestaurantCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; set; } = restaurantId;
}
