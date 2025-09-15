using MediatR;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Application.Requests;

public record GetUserRequest(long Id) : IRequest<UserResponseDto>;