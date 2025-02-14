using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SolanaMusicApi.Domain.DTO.Auth;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;
using System.Text;

namespace solana_music_api.Extensions;

public static class AuthenticationExtension
{
    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtTokenSettings>(builder.Configuration.GetSection(nameof(JwtTokenSettings)));
        builder.Services.Configure<GoogleAuthSettings>(builder.Configuration.GetSection("Authentication/Google"));
        ConfigureAurhProvider(builder);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = builder.Configuration.GetSection(nameof(JwtTokenSettings)).Get<JwtTokenSettings>();
            if (jwtSettings == null)
                throw new ArgumentNullException(nameof(jwtSettings));

            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtSettings.JwtAudience,
                ValidIssuer = jwtSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtKey)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        })
        .AddGoogle(options =>
        {
            var googleSettings = builder.Configuration.GetSection("Authentication:Google").Get<GoogleAuthSettings>();
            if (googleSettings == null)
                throw new ArgumentNullException(nameof(googleSettings));

            options.ClientId = googleSettings.ClientId;
            options.ClientSecret = googleSettings.ClientSecret;
            options.CallbackPath = "/signin-google";
            options.Scope.Add("email");
            options.Scope.Add("profile");
        });
    }

    private static void ConfigureAurhProvider(WebApplicationBuilder builder)
    {
        var authProviders = builder.Configuration
                           .GetSection("Authentication")
                           .GetChildren()
                           .Select(section => section.Key)
                           .ToList();

        builder.Services.Configure<AuthSettings>(options => options.SupportedProviders = authProviders);
    }
}
