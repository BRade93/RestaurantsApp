using Restaurants.Domain.Entities.User;

namespace Restaurants.Domain;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    public Address? Address { get; set; }
    public List<Dish>? Dishes { get; set; }

    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
}