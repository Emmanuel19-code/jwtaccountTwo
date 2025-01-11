using jwtaccount_two.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace jwtaccount_two.Service
{
    public interface IWeatherService
    {
        WeatherResponse? GetParticularDayWeather(RequestWeather request);
    }
}
