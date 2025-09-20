using MediatR;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Application.Requests;

public record GetUserRequest(long Id) : IRequest<UserResponseDto>;
public record UpdateUserRequest(long Id, UpdateUserDto UpdateUserDto) : IRequest<UserResponseDto>;
public record DeleteUserRequest(long Id) : IRequest;