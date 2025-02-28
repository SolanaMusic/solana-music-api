using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.DTO.User.Profile;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Application.Services.UserProfileService;

public interface IUserProfileService : IBaseService<UserProfile>
{
    Task CreateUserProfileAsync(long userId, Country country, UserProfileRequestDto userProfileRequestDto);
    Task CreateUserProfileAsync(long userId, Country country, ExternalLoginInfo info);
}
