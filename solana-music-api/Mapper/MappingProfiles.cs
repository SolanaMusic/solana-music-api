using AutoMapper;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.Entities.User;

namespace solana_music_api.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterDto, LoginDto>();
        CreateMap<RegisterDto, ApplicationUser>();
    }
}
