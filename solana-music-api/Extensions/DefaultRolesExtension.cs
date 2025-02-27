using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Extensions;

public static class DefaultRolesExtension
{
    public static async Task CreateDefaultRolesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();

        var roles = new List<string>
        {
            nameof(UserRoles.User),
            nameof(UserRoles.Artist),
            nameof(UserRoles.Moderator),
            nameof(UserRoles.Admin)
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<long> { Name = role });
        }
    }
}