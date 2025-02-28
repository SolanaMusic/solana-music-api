using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using SolanaMusicApi.Application;
using SolanaMusicApi.Application.Factories.RedirectUrlFactory;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.CountryService;
using SolanaMusicApi.Application.Services.LocationService;
using SolanaMusicApi.Application.Services.TracksService;
using SolanaMusicApi.Application.Services.UserProfileService;
using SolanaMusicApi.Application.Services.UserService;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;
using SolanaMusicApi.Infrastructure.Repositories.CountryRepository;
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
        services.AddScoped<ITrackRepository, TrackRepository>();
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IRedirectUrlFactory, RedirectUrlFactory>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ICountryService, CountryService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<ITracksService, TracksService>();
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
