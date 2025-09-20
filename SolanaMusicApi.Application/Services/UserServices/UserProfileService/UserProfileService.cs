using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.UserServices.UserProfileService;

public class UserProfileService(IBaseRepository<UserProfile> baseRepository)
    : BaseService<UserProfile>(baseRepository), IUserProfileService
{
    public async Task CreateUserProfileAsync(long userId, Country country, string? avatarUrl = null)
    {
        var profile = new UserProfile
        {
            UserId = userId,
            AvatarUrl = avatarUrl,
            CountryId = country.Id,
            TokensAmount = 0,
        };

        await AddAsync(profile);
    }
}
