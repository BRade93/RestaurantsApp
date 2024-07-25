using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application.Dishes.GetDishes;

public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dishes for restaurant with id: {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var result = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
        return result;
    }
}
