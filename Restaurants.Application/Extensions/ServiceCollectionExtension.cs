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
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssemblies(assembly)
        .AddFluentValidationAutoValidation();
    }
}
