namespace Restaurants.Domain;

public class NotFoundException(string resourceType, string resourceIdentifier) : Exception($"{resourceType} with id {resourceIdentifier} doesnt exist.")
{
}
