﻿using FluentValidation;

namespace Restaurants.Application;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 100);
    }
}
