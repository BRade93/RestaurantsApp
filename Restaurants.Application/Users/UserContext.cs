using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User ?? throw new InvalidOperationException("User context is not present");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }
        var userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(x => x.Type == ClaimTypes.Role)!.Select(c => c.Value);
        var nationality = user.FindFirst(x => x.Type == "Nationality")?.Value;
        var dateOfBirthString = user.FindFirst(x => x.Type == "DateOfBirth")?.Value;
        var dateOfBirth = dateOfBirthString == null ? (DateOnly?)null : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");

        return new CurrentUser(userId, email, roles, dateOfBirthString, dateOfBirth);
    }
}
