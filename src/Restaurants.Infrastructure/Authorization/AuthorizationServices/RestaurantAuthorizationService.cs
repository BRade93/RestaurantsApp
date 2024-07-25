using Microsoft.Extensions.Logging;
using Restaurants.Application;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.AuthorizationServices;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation operation)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user {UserEmail} to {Operation} for restaurant {RestaurantName}", user.Email, operation, restaurant.Name);

        if (operation == ResourceOperation.Create || operation == ResourceOperation.Read)
        {
            logger.LogInformation("Create/Read operation - sucessfull operation");
            return true;
        }
        if (operation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - sucessfull operation");
            return true;
        }
        if (operation == ResourceOperation.Delete || operation == ResourceOperation.Update && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - sucessfull operation");
            return true;
        }

        return false;
    }
}
