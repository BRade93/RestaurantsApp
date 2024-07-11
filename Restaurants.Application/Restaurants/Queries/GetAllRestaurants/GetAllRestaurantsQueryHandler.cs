using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain;

namespace Restaurants.Application;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQuery> logger, IMapper mapper, IRestaurantRepository repository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
    }
}
