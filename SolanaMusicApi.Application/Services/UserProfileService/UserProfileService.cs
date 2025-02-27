using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.UserProfileService;

public class UserProfileService : BaseService<UserProfile>, IUserProfileService
{
    public UserProfileService(IBaseRepository<UserProfile> baseRepository) : base(baseRepository) { }
}
