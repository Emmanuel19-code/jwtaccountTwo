using AutoMapper;
using jwtaccount_two.Domain.Contracts;


namespace jwtaccount_two.Service
{
    public class WeatherService : IWeatherService
    {
       private readonly ILogger<WeatherService> _logger;
       private readonly IMapper _mapper;
       public WeatherService(ILogger<WeatherService> logger,IMapper mapper)
       {
            _logger = logger;
            _mapper = mapper;
       }
        public WeatherResponse? GetParticularDayWeather(RequestWeather request)
        {
            if (string.IsNullOrEmpty(request.Date))
            {
               return null;
            }
            
            var weatherInfo = new WeatherInfo
            {
                Type = "Autum",
                Info = "This is the info",
                Description = "working on description"
            };
           // return weather;
           //return _mapper.Map<WeatherInfo, WeatherResponse>(weatherInfo);
           return _mapper.Map<WeatherResponse>(weatherInfo);
        }
    }
}
