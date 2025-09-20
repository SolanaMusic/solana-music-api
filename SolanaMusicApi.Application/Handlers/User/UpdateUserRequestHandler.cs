using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.UserServices.UserService;
using SolanaMusicApi.Domain.DTO.User;

namespace SolanaMusicApi.Application.Handlers.User;

public class UpdateUserRequestHandler(IUserService userService, IMapper mapper) :  IRequestHandler<UpdateUserRequest, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await userService.UpdateUserAsync(request.Id, request.UpdateUserDto);
        return mapper.Map<UserResponseDto>(response);
    }
}