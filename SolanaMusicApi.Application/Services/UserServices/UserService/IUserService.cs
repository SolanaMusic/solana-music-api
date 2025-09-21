using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Domain.Entities.User;
using System.Linq.Expressions;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Application.Services.UserServices.UserService;

public interface IUserService
{
    IQueryable<ApplicationUser> GetUsers();
    Task<ApplicationUser?> GetUserAsync(Expression<Func<ApplicationUser, bool>> expression);
    Task<string> GetUserRoleAsync(ApplicationUser user);
    Task<string> GetUserRoleAsync(long id);
    Task CreateUserAsync(ApplicationUser user, string? password = null);
    Task<ApplicationUser> UpdateUserAsync(long id, UpdateUserDto updateUserDto, string? role = null);
    Task UpdateUserRoleAsync(long id, UserRoles newRole);
    Task DeleteUserAsync(long id);
    string AggregateErrors(IEnumerable<IdentityError> errors);
}
