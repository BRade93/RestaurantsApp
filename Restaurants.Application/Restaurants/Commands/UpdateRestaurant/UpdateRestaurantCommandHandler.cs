using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommand> logger, IRestaurantRepository repository, IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with id : {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
        var restaurant = await repository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        mapper.Map(request, restaurant);

        await repository.Update();
    }
}
