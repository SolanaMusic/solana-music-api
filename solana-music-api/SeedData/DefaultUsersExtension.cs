using Microsoft.AspNetCore.Identity;
using solana_music_api.SeedData.Constatnts;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums;

namespace solana_music_api.SeedData;

public static class DefaultUsersExtension
{
    public static async Task CreateDefaultUsersAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var adminUser = await userManager.FindByEmailAsync(DefaultUsers.UserName);
        var moderUser = await userManager.FindByEmailAsync(DefaultUsers.ModerEmail);
        var defaultUser = await userManager.FindByEmailAsync(DefaultUsers.AdminEmail);

        if (defaultUser == null)
        {
            var user = new ApplicationUser
            {
                Email = DefaultUsers.UserEmail,
                UserName = DefaultUsers.UserName,
            };

            var userResponse = await userManager.CreateAsync(user, DefaultUsers.UserPassword);
            if (userResponse.Succeeded)
                await userManager.AddToRoleAsync(user, nameof(UserRoles.User));
        }

        if (defaultUser == null)
        {
            var user = new ApplicationUser
            {
                Email = DefaultUsers.ModerEmail,
                UserName = DefaultUsers.ModerName,
            };

            var userResponse = await userManager.CreateAsync(user, DefaultUsers.ModerPassword);
            if (userResponse.Succeeded)
                await userManager.AddToRoleAsync(user, nameof(UserRoles.Moderator));
        }

        if (adminUser == null)
        {
            var admin = new ApplicationUser
            {
                Email = DefaultUsers.AdminEmail,
                UserName = DefaultUsers.AdminName,
            };

            var adminResponse = await userManager.CreateAsync(admin, DefaultUsers.AdminPassword);

            if (adminResponse.Succeeded)
                await userManager.AddToRoleAsync(admin, nameof(UserRoles.Admin));
        }
    }
}
