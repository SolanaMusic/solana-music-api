using AutoMapper;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.General.CountryDto;
using SolanaMusicApi.Domain.DTO.Genre;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.DTO.User.Profile;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Entities.User;

namespace solana_music_api.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterDto, LoginDto>();
        CreateMap<LoginDto, ApplicationUser>();

        CreateMap<CountryRequestDto, Country>();
        CreateMap<Country, CountryResponseDto>();

        CreateMap<UserProfile, UserProfileResponseDto>();
        CreateMap<ApplicationUser, UserResponseDto>();
        CreateMap<LoginResponseDto, AuthResponseDto>();

        CreateMap<GenreRequestDto, Genre>();
        CreateMap<Genre, GenreResponseDto>();

        CreateMap<TrackRequestDto, Track>();
        CreateMap<Track, TrackResponseDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.TrackGenres.Select(tg => tg.Genre)))
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.ArtistTracks.Select(at => at.Artist)));
    }
}
