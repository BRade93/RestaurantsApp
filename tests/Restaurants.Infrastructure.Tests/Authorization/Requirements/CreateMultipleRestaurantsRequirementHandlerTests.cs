using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application;
using Restaurants.Domain;

namespace Restaurants.Infrastructure.Tests.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementHandlerTests
{
    [Fact()]
    public async Task HandleRequirementAsync_UserCreatedMultipleRestaurants_ShouldSucceed()
    {
        //arrange
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = "2",
            }
        };

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreateMultipleRestaurantsRequirement(2);
        var handler = new CreateMultipleRestaurantsRequirementHandler(restaurantRepositoryMock.Object, userContextMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        //act
        await handler.HandleAsync(context);

        //assert
        context.HasSucceeded.Should().BeTrue();
    }
    [Fact()]
    public async Task HandleRequirementAsync_UserCreatedMultipleRestaurants_ShouldFail()
    {
        //arrange
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = "2",
            }
        };

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreateMultipleRestaurantsRequirement(2);
        var handler = new CreateMultipleRestaurantsRequirementHandler(restaurantRepositoryMock.Object, userContextMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        //act
        await handler.HandleAsync(context);

        //assert
        context.HasSucceeded.Should().BeFalse();
    }
}
