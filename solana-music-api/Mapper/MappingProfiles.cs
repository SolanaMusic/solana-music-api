using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.Constants;
using SolanaMusicApi.Domain.DTO.Album;
using SolanaMusicApi.Domain.DTO.Artist;
using SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;
using SolanaMusicApi.Domain.DTO.ArtistTrack;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.Auth.Default;
using SolanaMusicApi.Domain.DTO.Country;
using SolanaMusicApi.Domain.DTO.Currency;
using SolanaMusicApi.Domain.DTO.Genre;
using SolanaMusicApi.Domain.DTO.Nft.LikedNft;
using SolanaMusicApi.Domain.DTO.Nft.Nft;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;
using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.DTO.Playlist;
using SolanaMusicApi.Domain.DTO.Subscription;
using SolanaMusicApi.Domain.DTO.SubscriptionPlan;
using SolanaMusicApi.Domain.DTO.SubscriptionPlanCurrency;
using SolanaMusicApi.Domain.DTO.Track;
using SolanaMusicApi.Domain.DTO.Track.RecentlyPlayed;
using SolanaMusicApi.Domain.DTO.Transaction;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.DTO.User.Profile;
using SolanaMusicApi.Domain.DTO.Whitelist;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.Music;
using SolanaMusicApi.Domain.Entities.Nft;
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
        CreateMap<WhitelistRequestDto, Whitelist>();
        CreateMap<ApplicationUser, IdentityUser<long>>();
        CreateMap<ApplicationUser, UserResponseDto>()
            .ForMember(dest => dest.Following, opt => opt.MapFrom(src => src.ArtistSubscribes.Count))
            .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
            .AfterMap((src, dest) => {
                if (src.Artist != null && !string.IsNullOrEmpty(src.Artist.ImageUrl))
                    dest.Profile.AvatarUrl = src.Artist.ImageUrl;
            });

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
            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.Album != null ? src.Album.ImageUrl : src.ImageUrl))
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.ArtistTracks.Select(at => at.Artist)));

        CreateMap<RecentlyPlayed, RecentlyPlayedResponseDto>();
        CreateMap<RecentlyPlayedRequestDto, RecentlyPlayed>();
        CreateMap<AddOrUpdateRecentlyPlayedRequest, RecentlyPlayed>();
        
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
        CreateMap<Album, GetTrackAlbumResponseDto>()
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.ArtistAlbums.Select(x => x.Artist)))
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
        CreateMap<Playlist, GetUserPlaylistResponseDto>()
            .ForMember(dest => dest.TracksCount, opt => opt.MapFrom(src => src.PlaylistTracks.Count))
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
        CreateMap<Artist, ArtistSlimResponseDto>();
        CreateMap<Artist, ArtistResponseDto>()
            .ForMember(dest => dest.SubscribersCount, opt => opt.MapFrom(src => src.ArtistSubscribers.Count))
            .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.ArtistAlbums.Select(x => x.Album)))
            .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.ArtistTracks.Select(x => x.Track)));

        CreateMap<ArtistApplicationRequestDto, ArtistApplication>();
        CreateMap<UpdateArtistApplicationDto, ArtistApplication>();
        CreateMap<ArtistApplication, ArtistApplicationResponseDto>();
        CreateMap<ArtistApplication, UserArtistApplicationDto>();

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

        CreateMap<Transaction, TransactionResponseDto>();
        CreateMap<Transaction, GetUserTransactionResponseDto>();

        CreateMap<NftRequestDto, Nft>();
        CreateMap<NftCollectionRequestDto, NftCollection>();
        
        CreateMap<Nft, NftResponseDto>()
            .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.Owner == Constants.SystemAddress));
        CreateMap<Nft, GetNftResponseDto>()
            .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.Owner == Constants.SystemAddress));
        
        CreateMap<NftCollection, NftCollectionResponseDto>()
            .ForMember(dest => dest.Minted, opt => opt.MapFrom(src => src.Nfts.Count(x => x.Owner != Constants.SystemAddress)))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Nfts.Any() ? src.Nfts.Min(n => n.Price) : 0))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Nfts.Any() ? src.Nfts.First().Currency : null))
            .ForMember(dest => dest.Creators, opt => opt.MapFrom(src => GetCreators(src)));
        
        CreateMap<LikedNftRequestDto, LikedNft>();
        CreateMap<LikedNft, LikedNftResponseDto>();

        CreateMap(typeof(ResponsePaginationDto<>), typeof(PaginationResponseDto<>));
    }
    
    private static List<ArtistResponseDto> GetCreators(NftCollection src)
    {
        List<ArtistResponseDto> artists = [];

        if (src.Album != null)
        {
            artists.AddRange(src.Album.ArtistAlbums
                .Select(x => MapArtist(x.Artist)));
            return artists;
        }

        if (src.Track != null)
        {
            artists.AddRange(src.Track.ArtistTracks
                .Select(x => MapArtist(x.Artist)));
            return artists;
        }

        if (src.Artist != null)
            artists.Add(MapArtist(src.Artist));

        return artists;
    }
    
    private static ArtistResponseDto MapArtist(Artist artist) =>
        new()
        {
            Id = artist.Id,
            Name = artist.Name,
            Country = new CountryResponseDto
            {
                Id = artist.Country.Id,
                Name = artist.Country.Name,
                CountryCode = artist.Country.CountryCode
            }
        };
}
