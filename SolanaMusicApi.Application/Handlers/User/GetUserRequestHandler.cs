using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.UserServices.UserService;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Application.Handlers.User;

public class GetUserRequestHandler(IUserService userService, IMapper mapper) : IRequestHandler<GetUserRequest, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var response = await userService.GetUserAsync(x => x.Id == request.Id);
        return mapper.Map<UserResponseDto>(response);
    }
}