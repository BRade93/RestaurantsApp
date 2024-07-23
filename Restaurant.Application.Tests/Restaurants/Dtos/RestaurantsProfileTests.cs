using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Dtos;
using Restaurants.Application.MappingProfiles;
using Restaurants.Domain;

namespace Restaurants.Application.Tests.Restaurants.Dtos;

public class RestaurantProfileTests
{
    private readonly IMapper _mapper;
    public RestaurantProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantsProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForRestaurantToRestaurantDTO_MapsCorrectly()
    {
        // Arrange

        var restaurant = new Domain.Restaurant()
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "555222234",
            Address = new Address
            {
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12345"
            },
        };

        // Act
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        // Assert
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.Street.Should().Be(restaurant.Address.Street);
        restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
    }

    [Fact]
    public void CreateMap_ForCreateRestaurantCommand_MapsCorrectly()
    {
        // Arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "555222234",
            City = "Test City",
            Street = "Test Street",
            PostalCode = "12345"
        };

        // Act
        var restaurant = _mapper.Map<Domain.Restaurant>(command);

        // Assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.Category.Should().Be(command.Category);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
        restaurant.ContactEmail.Should().Be(command.ContactEmail);
        restaurant.ContactNumber.Should().Be(command.ContactNumber);
        restaurant.Address.City.Should().Be(command.City);
        restaurant.Address.Street.Should().Be(command.Street);
        restaurant.Address.PostalCode.Should().Be(command.PostalCode);
    }

    [Fact]
    public void CreateMap_ForUpdateRestaurantCommand_MapsCorrectly()
    {
        // Arrange

        var command = new UpdateRestaurantCommand()
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "Test Description",
            HasDelivery = true,
        };

        // Act
        var restaurant = _mapper.Map<Domain.Restaurant>(command);

        // Assert
        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(command.Id);
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
    }
}
