using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolanaMusicApi.Domain.DTO.Auth;
using System.Text;

namespace solana_music_api.Extensions;

public static class AuthenticationExtension
{
    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtTokenSettings>(builder.Configuration.GetSection(nameof(JwtTokenSettings)));
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                using var scope = builder.Services.BuildServiceProvider().CreateScope();
                var jwtSettings = scope.ServiceProvider.GetRequiredService<IOptions<JwtTokenSettings>>().Value;

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
            });
    }
}
