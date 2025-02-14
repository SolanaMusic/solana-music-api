using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using SolanaMusicApi.Application;
using SolanaMusicApi.Application.Factories.RedirectUrlFactory;
using SolanaMusicApi.Application.Services.AuthService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.User;

namespace solana_music_api;

public static class Configurator
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {

    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IRedirectUrlFactory, RedirectUrlFactory>();
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void ConfigureGeneral(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IBaseService<>).Assembly));
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<RoleManager<IdentityRole>>();
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
