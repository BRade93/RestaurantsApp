using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Tests.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly UpdateRestaurantCommandHandler _handler;
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantRepository> _restaurantRepositoryMock;
    private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationMock;

    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        _restaurantAuthorizationMock = new Mock<IRestaurantAuthorizationService>();

        _handler = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _restaurantRepositoryMock.Object,
            _mapperMock.Object,
            _restaurantAuthorizationMock.Object
        );

    }
    [Fact]
    public async Task Handler_WithValidRequest_ShouldUpdateRestaurant()
    {
        //arrange 
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
            Name = "New test",
            Description = "Description test",
            HasDelivery = true,
        };
        var restaurant = new Domain.Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test",
        };

        _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(restaurantId))
            .ReturnsAsync(restaurant);

        _restaurantAuthorizationMock.Setup(x => x.Authorize(restaurant, ResourceOperation.Update))
            .Returns(true);

        //act

        await _handler.Handle(command, CancellationToken.None);

        //assert
        _restaurantRepositoryMock.Verify(r => r.Update(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);

    }
    [Fact]
    public async Task Handler_WithNonExistingRestaurant_ShouldThrowNotFoundException()
    {
        //arrange 
        var restaurantId = 2;
        var request = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
        };

        _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(restaurantId))
            .ReturnsAsync((Domain.Restaurant?)null);

        //act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id {restaurantId} doesnt exist.");

    }
    [Fact]
    public async Task Handler_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        //arrange 
        var restaurantId = 3;
        var request = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
        };
        var existingRestaurant = new Domain.Restaurant()
        {
            Id = restaurantId,
        };

        _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(restaurantId))
            .ReturnsAsync(existingRestaurant);
        _restaurantAuthorizationMock.Setup(x => x.Authorize(existingRestaurant, ResourceOperation.Update))
           .Returns(false);
        //act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<ForbidException>();

    }
}
