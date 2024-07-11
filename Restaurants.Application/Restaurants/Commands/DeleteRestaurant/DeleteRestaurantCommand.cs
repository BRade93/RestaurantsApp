using MediatR;

namespace Restaurants.Application;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
