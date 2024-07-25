using AutoMapper;
using Restaurants.Application.Dishes.CreateDish;
using Restaurants.Domain;

namespace Restaurants.Application;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>();
        CreateMap<CreateDishCommand, Dish>();
    }

}
