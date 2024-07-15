using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure;

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; set; } = minimumAge;
}
