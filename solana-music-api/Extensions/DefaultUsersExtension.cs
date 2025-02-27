using Microsoft.AspNetCore.Identity;
using solana_music_api.SeedData;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums;

namespace solana_music_api.Extensions;

public static class DefaultUsersExtension
{
    public static async Task CreateDefaultUsersAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var users = new[]
        {
            new { Email = DefaultUsers.UserEmail, UserName = DefaultUsers.UserName, Password = DefaultUsers.UserPassword, Role = UserRoles.User },
            new { Email = DefaultUsers.ArtistEmail, UserName = DefaultUsers.ArtistName, Password = DefaultUsers.ArtistPassword, Role = UserRoles.Artist },
            new { Email = DefaultUsers.ModerEmail, UserName = DefaultUsers.ModerName, Password = DefaultUsers.ModerPassword, Role = UserRoles.Moderator },
            new { Email = DefaultUsers.AdminEmail, UserName = DefaultUsers.AdminName, Password = DefaultUsers.AdminPassword, Role = UserRoles.Admin }
        };

        foreach (var userData in users)
        {
            if (await userManager.FindByEmailAsync(userData.Email) == null)
            {
                var user = new ApplicationUser { Email = userData.Email, UserName = userData.UserName };
                var result = await userManager.CreateAsync(user, userData.Password);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, userData.Role.ToString());
            }
        }
    }
}
