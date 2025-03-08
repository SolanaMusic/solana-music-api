using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using SolanaMusicApi.Application;
using SolanaMusicApi.Application.Factories.FilePathFactory;
using SolanaMusicApi.Application.Factories.RedirectUrlFactory;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Application.Services.ArtistAlbumService;
using SolanaMusicApi.Application.Services.ArtistService;
using SolanaMusicApi.Application.Services.ArtistTrackService;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Application.Services.CurrencyService;
using SolanaMusicApi.Application.Services.FileService;
using SolanaMusicApi.Application.Services.GenreService;
using SolanaMusicApi.Application.Services.LocationService;
using SolanaMusicApi.Application.Services.SubscriptionPlanService;
using SolanaMusicApi.Application.Services.TrackGenreService;
using SolanaMusicApi.Application.Services.TracksService;
using SolanaMusicApi.Application.Services.UserProfileService;
using SolanaMusicApi.Application.Services.UserService;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Repositories.AlbumRepository;
using SolanaMusicApi.Infrastructure.Repositories.ArtistAlbumRepository;
using SolanaMusicApi.Infrastructure.Repositories.ArtistRepository;
using SolanaMusicApi.Infrastructure.Repositories.ArtistTrackRepository;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;
using SolanaMusicApi.Infrastructure.Repositories.CountryRepository;
using SolanaMusicApi.Infrastructure.Repositories.CurrencyRepository;
using SolanaMusicApi.Infrastructure.Repositories.GenreRepository;
using SolanaMusicApi.Infrastructure.Repositories.SubscriptionPlanRepository;
using SolanaMusicApi.Infrastructure.Repositories.TrackGenreRepository;
using SolanaMusicApi.Infrastructure.Repositories.TrackRepository;
using SolanaMusicApi.Infrastructure.Repositories.UserProfileRepository;

namespace solana_music_api;

public static class Configurator
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICountryRepository, CountryRepository>();

        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<ITrackGenreRepository, TrackGenreRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IArtistTrackRepository, ArtistTrackRepository>();
        services.AddScoped<IArtistAlbumRepository, ArtistAlbumRepository>();

        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IRedirectUrlFactory, RedirectUrlFactory>();
        services.AddScoped<IFilePathFactory, FilePathFactory>();

        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ICountryService, CountryService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserProfileService, UserProfileService>();

        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ITracksService, TracksService>();
        services.AddScoped<ITrackGenreService, TrackGenreService>();
        services.AddScoped<IAlbumService, AlbumService>();
        services.AddScoped<IArtistTrackService, ArtistTrackService>();
        services.AddScoped<IArtistAlbumService, ArtistAlbumService>();

        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
    }

    public static void ConfigureGeneral(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IBaseService<>).Assembly));
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
                    new string[] { }
                }
            });
        });
    }
}
