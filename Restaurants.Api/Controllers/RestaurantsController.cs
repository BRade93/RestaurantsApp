using Microsoft.AspNetCore.Mvc;
using Restaurants.Application;
using Restaurants.Application.Dtos;
using Restaurants.Domain;

namespace Restaurants.Api;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await service.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Restaurant>> GetById(int id)
    {
        var restaurant = await service.GetRestaurantById(id);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }
    [HttpPost]
    public async Task<IActionResult> AddRestaurant(CreateRestaurantDto restaurantDto)
    {
        int id = await service.Create(restaurantDto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}
