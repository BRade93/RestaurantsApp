using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IRestaurantRepository repository, IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with id : {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
        var restaurant = await repository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        mapper.Map(request, restaurant);

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }
        await repository.Update();
    }
}
