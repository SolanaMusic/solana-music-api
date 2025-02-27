using AutoMapper;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.General.CountryDto;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.User;

namespace solana_music_api.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterDto, LoginDto>();
        CreateMap<RegisterDto, ApplicationUser>();

        CreateMap<CountryRequestDto, Country>();
        CreateMap<Country, CountryResponseDto>();

        CreateMap<UserProfile, UserProfileResponseDto>();
        CreateMap<ApplicationUser, UserResponseDto>();
        CreateMap<LoginResponseDto, AuthResponseDto>();
    }
}
