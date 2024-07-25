using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
        var domainAssembly = typeof(Restaurant).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddAutoMapper(applicationAssembly, domainAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly);
        services.AddValidatorsFromAssembly(domainAssembly);
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }
}
