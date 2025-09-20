using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Application.Services.UserServices.UserProfileService;

public interface IUserProfileService : IBaseService<UserProfile>
{
    Task CreateUserProfileAsync(long userId, Country country, string? avatarUrl = null);
}
