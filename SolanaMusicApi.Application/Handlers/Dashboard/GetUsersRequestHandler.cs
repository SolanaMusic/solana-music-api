using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.UserServices.UserService;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Application.Handlers.Dashboard;

public class GetUsersRequestHandler(IUserService userService, IMapper mapper) : IRequestHandler<GetUsersRequest, List<UserResponseDto>>
{
    public Task<List<UserResponseDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = userService.GetUsers();
        var response = mapper.Map<List<UserResponseDto>>(users);
        return Task.FromResult(response);
    }
}