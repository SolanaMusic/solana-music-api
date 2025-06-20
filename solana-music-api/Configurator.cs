using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using SolanaMusicApi.Application.Factories.FilePathFactory;
using SolanaMusicApi.Application.Factories.RedirectUrlFactory;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistAlbumService;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistSubscriberService;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistTrackService;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Application.Services.CurrencyService;
using SolanaMusicApi.Application.Services.DashboardService;
using SolanaMusicApi.Application.Services.FileService;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Application.Services.LocationService;
using SolanaMusicApi.Application.Services.NftServices.NftCollectionService;
using SolanaMusicApi.Application.Services.NftServices.NftService;
using SolanaMusicApi.Application.Services.PaymentServices.PaymentService;
using SolanaMusicApi.Application.Services.PaymentServices.TransactionService;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;
using SolanaMusicApi.Application.Services.PlaylistServices.PlaylistTrackService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanCurrencyService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionPlanService;
using SolanaMusicApi.Application.Services.SubscriptionServices.SubscriptionService;
using SolanaMusicApi.Application.Services.SubscriptionServices.UserSubscriptionService;
using SolanaMusicApi.Application.Services.TrackServices.TrackGenreService;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Application.Services.UserServices.UserProfileService;
using SolanaMusicApi.Application.Services.UserServices.UserService;
using SolanaMusicApi.Application.Services.WhitelistService;
using SolanaMusicApi.Domain.DTO.Country;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure;
using SolanaMusicApi.Infrastructure.Repositories.AlbumRepository;
using SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistAlbumRepository;
using SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistRepository;
using SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistSubscriberRepository;
using SolanaMusicApi.Infrastructure.Repositories.ArtistRepositories.ArtistTrackRepository;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;
using SolanaMusicApi.Infrastructure.Repositories.CountryRepository;
using SolanaMusicApi.Infrastructure.Repositories.CurrencyRepository;
using SolanaMusicApi.Infrastructure.Repositories.GenreRepository;
using SolanaMusicApi.Infrastructure.Repositories.NftRepositories.NftCollectionRepository;
using SolanaMusicApi.Infrastructure.Repositories.NftRepositories.NftRepository;
using SolanaMusicApi.Infrastructure.Repositories.PlaylistRepositories.PlaylistRepository;
using SolanaMusicApi.Infrastructure.Repositories.PlaylistRepositories.PlaylistTrackRepository;
using SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionPlanCurrencyRepository;
using SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionPlanRepository;
using SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.SubscriptionRepository;
using SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.UserSubscriptionRepository;
using SolanaMusicApi.Infrastructure.Repositories.TrackRepositories.TrackGenreRepository;
using SolanaMusicApi.Infrastructure.Repositories.TrackRepositories.TrackRepository;
using SolanaMusicApi.Infrastructure.Repositories.TransactionRepository;
using SolanaMusicApi.Infrastructure.Repositories.UserProfileRepository;
using SolanaMusicApi.Infrastructure.Repositories.WhitelistRepository;

namespace solana_music_api;

public static class Configurator
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IWhitelistRepository, WhitelistRepository>();

        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<ITrackGenreRepository, TrackGenreRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<IArtistTrackRepository, ArtistTrackRepository>();
        services.AddScoped<IArtistAlbumRepository, ArtistAlbumRepository>();
        services.AddScoped<IArtistSubscriberRepository, ArtistSubscriberRepository>();
        services.AddScoped<IPlaylistRepository, PlaylistRepository>();
        services.AddScoped<IPlaylistTrackRepository, PlaylistTrackRepository>();

        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
        services.AddScoped<ISubscriptionPlanCurrencyRepository, SubscriptionPlanCurrencyRepository>();
        services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<INftCollectionRepository, NftCollectionRepository>();
        services.AddScoped<INftRepository, NftRepository>();
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IRedirectUrlFactory, RedirectUrlFactory>();
        services.AddScoped<IFilePathFactory, FilePathFactory>();

        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IDashboardService, DashboardService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IWhitelistService, WhitelistService>();

        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ITracksService, TracksService>();
        services.AddScoped<ITrackGenreService, TrackGenreService>();
        services.AddScoped<IAlbumService, AlbumService>();
        services.AddScoped<IArtistService, ArtistService>();
        services.AddScoped<IArtistTrackService, ArtistTrackService>();
        services.AddScoped<IArtistAlbumService, ArtistAlbumService>();
        services.AddScoped<IArtistSubscriberService, ArtistSubscriberService>();
        services.AddScoped<IPlaylistService, PlaylistService>();
        services.AddScoped<IPlaylistTrackService, PlaylistTrackService>();

        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
        services.AddScoped<ISubscriptionPlanCurrencyService, SubscriptionPlanCurrencyService>();
        services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<INftCollectionService, NftCollectionService>();
        services.AddScoped<INftService, NftService>();
    }

    public static void ConfigureGeneral(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<LocationApiOptions>(builder.Configuration.GetSection("LocationApi"));
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder =>
                policyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IBaseService<>).Assembly));
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<long>>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<RoleManager<IdentityRole<long>>>();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    []
                }
            });
        });
    }
}
