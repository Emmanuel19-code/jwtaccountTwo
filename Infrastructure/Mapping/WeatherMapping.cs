using AutoMapper;
using jwtaccount_two.Domain.Contracts;

namespace jwtaccount_two.Infrastructure.Mapping
{
    public class WeatherMappingProfile : Profile
    {
        public WeatherMappingProfile()
        {
            CreateMap<WeatherInfo,WeatherResponse>();
        }
    }
}