using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Domain.Entities.User;
using System.Linq.Expressions;

namespace SolanaMusicApi.Application.Services.UserServices.UserService;

public interface IUserService
{
    Task<ApplicationUser> GetUserAsync(Expression<Func<ApplicationUser, bool>> expression);
    Task CreateUserAsync(ApplicationUser user, string? password = null);
    string AggregateErrors(IEnumerable<IdentityError> errors);
}
