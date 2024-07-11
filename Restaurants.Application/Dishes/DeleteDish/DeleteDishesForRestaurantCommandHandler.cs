using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DeleteDish;
using Restaurants.Domain;

namespace Restaurants.Application;

public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger, IRestaurantRepository restaurantRepository, IDishRepository dishRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId.ToString());

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        await dishRepository.Delete(restaurant.Dishes);
    }
}
