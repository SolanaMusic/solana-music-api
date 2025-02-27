using SolanaMusicApi.Domain.Entities.User;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.UserProfileRepository;

public interface IUserProfileRepository : IBaseRepository<UserProfile>;
