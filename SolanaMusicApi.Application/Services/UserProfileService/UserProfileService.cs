using Microsoft.AspNetCore.Identity;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.DTO.User.Profile;
using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;
using System.Text.RegularExpressions;

namespace SolanaMusicApi.Application.Services.UserProfileService;

public class UserProfileService : BaseService<UserProfile>, IUserProfileService
{
    public UserProfileService(IBaseRepository<UserProfile> baseRepository) : base(baseRepository) { }

    public async Task CreateUserProfileAsync(long userId, Country country, UserProfileRequestDto userProfileRequestDto)
    {
        var date = DateTime.UtcNow;
        var profile = new UserProfile
        {
            UserId = userId,
            AvatarUrl = userProfileRequestDto.AvatarUrl,
            CountryId = country.Id,
            TokensAmount = 0,
        };

        await AddAsync(profile);
    }

    public async Task CreateUserProfileAsync(long userId, Country country, ExternalLoginInfo info)
    {
        var date = DateTime.UtcNow;
        var profile = new UserProfile
        {
            UserId = userId,
            AvatarUrl = GetAvatarUrl(info),
            CountryId = country.Id,
            TokensAmount = 0,
        };

        await AddAsync(profile);
    }

    private string GetAvatarUrl(ExternalLoginInfo info)
    {
        var avatar = info.Principal.Claims.FirstOrDefault(c => c.Type == "urn:google:picture")?.Value ?? string.Empty;
        return Regex.Replace(avatar, @"=s\d+", "=s300");
    }
}
