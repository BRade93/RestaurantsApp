using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application;
using Restaurants.Application.Dtos;
using Restaurants.Domain;

namespace Restaurants.Api;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

        return Ok(restaurant);
    }
    [HttpPost]
    public async Task<IActionResult> AddRestaurant(CreateRestaurantCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Restaurant>> DeleteRestaurant(int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));

        return NotFound();
    }
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Restaurant>> UpdateRestaurant(int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NotFound();
    }
}
