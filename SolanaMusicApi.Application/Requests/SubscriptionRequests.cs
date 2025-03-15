using MediatR;
using SolanaMusicApi.Domain.DTO.Subscription;

namespace SolanaMusicApi.Application.Requests;

public record GetSubscriptionsRequest(long UserId) : IRequest<List<SubscriptionResponseDto>>;
public record GetSubscriptionRequest(SubscriptionActionDto SubscriptionActionDto) : IRequest<SubscriptionResponseDto>;
public record CreateSubscriptionRequest(SubscriptionRequestDto SubscriptionDto) : IRequest<SubscriptionResponseDto>;
public record AddToSubscriptionRequest(SubscriptionActionsDto SubscriptionActionsDto) : IRequest;
public record RemoveFromSubscriptionRequest(SubscriptionActionsDto SubscriptionActionsDto) : IRequest;
public record SelectSubscriptionRequest(SubscriptionActionDto SubscriptionActionDto) : IRequest;
public record ResubscribeRequest(long Id) : IRequest;
