using MediatR;
using Restaurants.Application.Dtos;

namespace Restaurants.Application;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
{
    
}
