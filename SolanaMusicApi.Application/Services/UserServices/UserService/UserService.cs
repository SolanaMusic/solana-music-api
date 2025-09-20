using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums;
using System.Linq.Expressions;
using SolanaMusicApi.Application.Services.FileService;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.Enums.File;

namespace SolanaMusicApi.Application.Services.UserServices.UserService;

public class UserService(UserManager<ApplicationUser> userManager, IFileService fileService) : IUserService
{
    public IQueryable<ApplicationUser> GetUsers()
    {
        return userManager.Users
            .Include(u => u.Profile)
                .ThenInclude(x => x.Country)
            .Include(x => x.Artist)
            .Include(x => x.UserSubscriptions)
                .ThenInclude(x => x.Subscription)
            .Include(x => x.Nfts)
            .Include(x => x.Playlists)
                .ThenInclude(x => x.PlaylistTracks)
                    .ThenInclude(x => x.Track)
            .Include(x => x.ArtistSubscribes)
            .Include(x => x.Transactions)
                .ThenInclude(x => x.Currency);
    }

    public async Task<ApplicationUser?> GetUserAsync(Expression<Func<ApplicationUser, bool>> expression) => 
        await GetUsers().FirstOrDefaultAsync(expression);
    
    public async Task<string> GetUserRoleAsync(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        return roles.First();
    }

    public async Task CreateUserAsync(ApplicationUser user, string? password = null)
    {
        var createUserResult = string.IsNullOrEmpty(password)
            ? await userManager.CreateAsync(user)
            : await userManager.CreateAsync(user, password);

        if (!createUserResult.Succeeded)
            throw new Exception(AggregateErrors(createUserResult.Errors));

        await UpdateUserRoleAsync(user, UserRoles.User);
    }

    public async Task<ApplicationUser> UpdateUserAsync(long id, UpdateUserDto updateUserDto, string? role = null)
    {
        string? avatarUrl = null;
        string? avatarSnapshot = null;

        try
        {
            var user = await GetUserByIdAsync(id);

            if (!string.IsNullOrEmpty(updateUserDto.UserName))
                user.UserName = updateUserDto.UserName;

            if (updateUserDto.TokensAmount.HasValue)
                user.Profile.TokensAmount = updateUserDto.TokensAmount.Value;

            if (updateUserDto.Avatar != null)
            {
                avatarUrl = await fileService.SaveFileAsync(updateUserDto.Avatar, FileTypes.UserImage);

                if (role != nameof(UserRoles.Artist))
                {
                    avatarSnapshot = user.Profile.AvatarUrl;
                    user.Profile.AvatarUrl = avatarUrl;
                }

                if (user.Artist != null)
                {
                    avatarSnapshot = user.Artist.ImageUrl;
                    user.Artist.ImageUrl = avatarUrl;
                }
            }
            
            await userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(avatarSnapshot) && !string.IsNullOrEmpty(avatarUrl)) 
                fileService.DeleteFile(avatarSnapshot);
            
            var response = await GetUserAsync(x => x.Id == user.Id);
            
            if (response == null)
                throw new Exception("User not found");
            
            return response;
        }
        catch
        {
            if (!string.IsNullOrEmpty(avatarUrl)) 
                fileService.DeleteFile(avatarUrl);
            
            throw;
        }
    }
    
    public async Task DeleteUserAsync(long id)
    {
        var user = await GetUserByIdAsync(id);
        var result = await userManager.DeleteAsync(user);
        
        if (!result.Succeeded)
            AggregateErrors(result.Errors);
        
        if (!string.IsNullOrEmpty(user.Profile.AvatarUrl))
            fileService.DeleteFile(user.Profile.AvatarUrl);
    }

    public string AggregateErrors(IEnumerable<IdentityError> errors) =>
        string.Join(" ", errors.Select(e => e.Description));

    private async Task<ApplicationUser> GetUserByIdAsync(long id)
    {
        return await userManager.Users
                   .Include(u => u.Profile)
                   .Include(x => x.Artist)
                   .FirstOrDefaultAsync(x => x.Id == id) 
               ?? throw new Exception("User not found");
    }
    
    private async Task UpdateUserRoleAsync(ApplicationUser user, UserRoles newRole)
    {
        var existingRoles = await userManager.GetRolesAsync(user);
        if (existingRoles.Contains(newRole.ToString()))
            return;

        await Task.WhenAll(existingRoles.Select(role => userManager.RemoveFromRoleAsync(user, role)));
        var roleResponse = await userManager.AddToRoleAsync(user, newRole.ToString());

        if (!roleResponse.Succeeded)
            throw new Exception(AggregateErrors(roleResponse.Errors));
    }
}
