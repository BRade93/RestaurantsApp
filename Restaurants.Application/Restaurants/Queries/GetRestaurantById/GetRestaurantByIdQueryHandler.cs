using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain;

namespace Restaurants.Application;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQuery> logger, IMapper mapper, IRestaurantRepository repository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting restaurant {RestaurantId}", request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        return mapper.Map<RestaurantDto>(restaurant);
    }
}
