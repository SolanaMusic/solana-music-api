using AutoMapper;
using SolanaMusicApi.Domain.DTO.Album;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.ArtistTrack;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.Currency;
using SolanaMusicApi.Domain.DTO.General.CountryDto;
using SolanaMusicApi.Domain.DTO.Genre;
using SolanaMusicApi.Domain.DTO.Playlist;
using SolanaMusicApi.Domain.DTO.Subscription;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;
using SolanaMusicApi.Domain.DTO.SubscriptionPlanCurrency;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.DTO.User.Profile;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Entities.Playlist;
using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Domain.Entities.Transaction;
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
        CreateMap<Track, GetAlbumTrackResponseDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.TrackGenres.Select(tg => tg.Genre)))
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.ArtistTracks.Select(at => at.Artist)));
        CreateMap<Track, GetArtistTrackResponseDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.TrackGenres.Select(tg => tg.Genre)));

        CreateMap<AlbumRequestDto, Album>();
        CreateMap<Album, AlbumResponseDto>()
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.ArtistAlbums.Select(x => x.Artist)))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Tracks
                .SelectMany(at => at.TrackGenres)
                .Select(tg => tg.Genre)
                .DistinctBy(g => g.Id)
            ))
            .ForMember(dest => dest.PlaysCount, opt => opt.MapFrom(src => src.Tracks.Sum(t => t.PlaysCount)));
        CreateMap<Album, GetArtistAlbumResponseDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Tracks
                .SelectMany(at => at.TrackGenres)
                .Select(tg => tg.Genre)
                .DistinctBy(g => g.Id)
            ))
            .ForMember(dest => dest.PlaysCount, opt => opt.MapFrom(src => src.Tracks.Sum(t => t.PlaysCount)));

        CreateMap<CreatePlaylistRequestDto, Playlist>();
        CreateMap<Playlist, PlaylistResponseDto>()
            .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.PlaylistTracks.Select(tg => tg.Track)))
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src =>
                !string.IsNullOrEmpty(src.CoverUrl)
                    ? src.CoverUrl
                    : src.PlaylistTracks
                        .OrderBy(pt => pt.TrackId)
                        .Select(pt => pt.Track.ImageUrl)
                        .FirstOrDefault()
            ));

        CreateMap<ArtistRequestDto, Artist>();
        CreateMap<Artist, ArtistTrackResponseDto>();
        CreateMap<Artist, GetAlbumArtistResponseDto>();
        CreateMap<Artist, ArtistResponseDto>()
            .ForMember(dest => dest.SubscribersCount, opt => opt.MapFrom(src => src.ArtistSubscribers.Count()))
            .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.ArtistAlbums.Select(x => x.Album)))
            .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.ArtistTracks.Select(x => x.Track)));

        CreateMap<CurrencyRequestDto, Currency>();
        CreateMap<Currency, CurrencyResponseDto>();

        CreateMap<CreateSubscriptionPlanRequestDto, SubscriptionPlan>();
        CreateMap<UpdateSubscriptionPlanRequestDto, SubscriptionPlan>();
        CreateMap<CreateSubscriptionPlanRequestDto, SubscriptionPlanCurrency>();
        CreateMap<SubscriptionPlanCurrencyRequestDto, SubscriptionPlanCurrency>();
        CreateMap<SubscriptionPlan, SubscriptionPlanResponseDto>();
        CreateMap<SubscriptionPlanCurrency, SubscriptionPlanCurrencyResponseDto>();
        CreateMap<SubscriptionRequestDto, Subscription>();
        CreateMap<Subscription, SubscriptionResponseDto>()
            .ForMember(dest => dest.Memebers, opt => opt.MapFrom(src => src.UserSubscriptions.Select(x => x.User)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src =>
                src.UpdatedDate.AddMonths(src.SubscriptionPlan.DurationInMonths).Date.AddDays(1)))
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
    }
}
