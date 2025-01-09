using AutoMapper;
using jwtaccount_two.Domain.Contracts;
using jwtaccount_two.Domain.Entities;

namespace jwtaccount_two.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser,UserResponse>();
            CreateMap<ApplicationUser,CurrentUserResponse>();
            CreateMap<UserRegisterRequest,ApplicationUser>();
        }
    }
}