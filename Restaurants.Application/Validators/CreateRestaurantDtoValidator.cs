using FluentValidation;
using Restaurants.Application.Dtos;

namespace Restaurants.Application;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
        .Length(3, 100);
        RuleFor(dto => dto.Category)
        .Must(validCategories.Contains)
        .WithMessage("Invalid category. Please choose from the valid categories.");
        // .Custom((value, context) =>
        // {
        //     var isValidCategory = validCategories.Contains(value);
        //     if (isValidCategory)
        //     {
        //         context.AddFailure("Category", "Invalid category. Please choose from the valid categories.");
        //     }
        // });

        RuleFor(dto => dto.ContactEmail)
        .EmailAddress()
        .WithMessage("Please provide valid email address.");

        RuleFor(dto => dto.PostalCode)
        .Matches(@"^\d{2}-\d{3}$")
        .WithMessage("Please provide valid postal code (XX-XXX)");


    }
}
