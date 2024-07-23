using FluentValidation.TestHelper;
using Restaurants.Application;

namespace Restaurant.Application.Tests.Restaurants.Commands;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        //Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "22-333"
        };
        var validator = new CreateRestaurantDtoValidator();
        //Act
        var result = validator.TestValidate(command);
        //Assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
    {
        //Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Category = "Ita",
            ContactEmail = "@test.com",
            PostalCode = "2233"
        };
        var validator = new CreateRestaurantDtoValidator();
        //Act
        var result = validator.TestValidate(command);
        //Assert

        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory()]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("American")]
    [InlineData("Indian")]
    public void Validator_ForValidCategory_ShoudlNotHaveAnyValidationErrorsForCategoryProperty(string category)
    {
        //Arrange
        var validator = new CreateRestaurantDtoValidator();
        var command = new CreateRestaurantCommand
        {
            Category = category,
        };
        //Act
        var result = validator.TestValidate(command);
        //Assert

        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("10222")]
    [InlineData("102-02")]
    [InlineData("10 222")]
    [InlineData("10-2 20")]
    public void Validator_ForInValidPostalCode_ShoudlHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        //Arrange
        var validator = new CreateRestaurantDtoValidator();
        var command = new CreateRestaurantCommand
        {
            PostalCode = postalCode,
        };
        //Act
        var result = validator.TestValidate(command);
        //Assert

        result.ShouldHaveValidationErrorFor(c => c.Category);
    }

}
