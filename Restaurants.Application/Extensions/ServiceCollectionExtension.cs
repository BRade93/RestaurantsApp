using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantsService>();
    }
}
