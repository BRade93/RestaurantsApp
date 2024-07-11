using MediatR;
using Restaurants.Application.Dtos;

namespace Restaurants.Application;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto>
{
    public int Id { get; set; } = id;
}
