using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies();
        services.AddScoped<IRestaurantService, RestaurantsService>();
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssemblies(assembly)
        .AddFluentValidationAutoValidation();
    }
}
