﻿using SolanaMusicApi.Domain.Entities.General;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.UserProfileRepositories.RecentlyPlayedRepository;

public interface IRecentlyPlayedRepository : IBaseRepository<RecentlyPlayed>;