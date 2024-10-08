﻿using FluentValidation;
using Restaurants.Application.Dishes.CreateDish;

namespace Restaurants.Application;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(dish => dish.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a non-negative number.");
            
        RuleFor(dish => dish.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kilo calories must be a non-negative number.");
    }
}
