using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.UserProfileRepository;

public class UserProfileRepository(ApplicationDbContext context) : BaseRepository<UserProfile>(context), IUserProfileRepository;
