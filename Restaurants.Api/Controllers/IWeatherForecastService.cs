namespace Restaurants.Api;

public interface IWeatherForecastService
{
    public IEnumerable<WeatherForecast> Get();
}
