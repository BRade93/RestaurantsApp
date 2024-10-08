﻿using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Application.Dtos;
using Restaurants.Domain;
using Xunit;

namespace Restaurants.Api.Tests.Controllers;

public class ControllersTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantRepository> _restaurantsRepositoryMock;
    public ControllersTests(WebApplicationFactory<Program> factory)
    {
        _restaurantsRepositoryMock = new Mock<IRestaurantRepository>();
        _factory = factory.WithWebHostBuilder(builder =>
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantRepository),
                                                    _ => _restaurantsRepositoryMock.Object));
            }));
    }
    [Fact]
    public async Task GetByID_ForExistingId_ShouldReturn200OK()
    {
        //arrange
        var id = 99;
        var restaurant = new Restaurant
        {
            Id = id,
            Name = "Test",
            Description = "Test Description"
        };

        _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(restaurant);

        var client = _factory.CreateClient();
        //act
        var response = await client.GetAsync($"/api/restaurants/{id}");
        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();
        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be("Test");
        restaurantDto.Description.Should().Be("Test Description");
    }
    [Fact]
    public async Task GetByID_ForNonExistingId_ShouldReturn404NotFound()
    {
        //arrange
        var id = 11123;

        _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);

        var client = _factory.CreateClient();
        //act
        var result = await client.GetAsync($"/api/restaurants/{id}");
        //assert
        //assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task GetAll_ForValidRequest_Returns200OK()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");

        //assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);

    }
    [Fact]
    public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var result = await client.GetAsync("/api/restaurants");

        //assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }
}
