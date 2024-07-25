using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantRepository repository, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{UserName} {UserId} is creating a new restaurant {@Restaurant}.", currentUser.Email, currentUser.Id, request);

        var restaurant = mapper.Map<Restaurant>(request);

        restaurant.OwnerId = currentUser.Id;
        int id = await repository.Create(restaurant);

        return id;
    }
}