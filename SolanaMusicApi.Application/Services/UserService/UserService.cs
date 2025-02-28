using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Domain.Enums;
using System.Linq.Expressions;

namespace SolanaMusicApi.Application.Services.UserService;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<ApplicationUser> GetUserAsync(Expression<Func<ApplicationUser, bool>> expression)
    {
        var response = await userManager.Users
            .Include(u => u.Profile)
            .FirstOrDefaultAsync(expression);

        if (response == null)
            throw new NullReferenceException("User not found");

        return response;
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

    public async Task UpdateUserRoleAsync(ApplicationUser user, UserRoles newRole)
    {
        var existingRoles = await userManager.GetRolesAsync(user);
        if (existingRoles.Contains(newRole.ToString()))
            return;

        await Task.WhenAll(existingRoles.Select(role => userManager.RemoveFromRoleAsync(user, role)));
        var roleResponse = await userManager.AddToRoleAsync(user, newRole.ToString());

        if (!roleResponse.Succeeded)
            throw new Exception(AggregateErrors(roleResponse.Errors));
    }

    public string AggregateErrors(IEnumerable<IdentityError> errors) =>
        string.Join(" ", errors.Select(e => e.Description));
}
