﻿using SolanaMusicApi.Domain.Entities.Subscription;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.SubscriptionRepositories.UserSubscriptionRepository;

public interface IUserSubscriptionRepository : IBaseRepository<UserSubscription>;
