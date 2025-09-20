using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.UserServices.UserService;

namespace SolanaMusicApi.Application.Handlers.User;

public class DeleteUserRequestHandler(IUserService userService) : IRequestHandler<DeleteUserRequest>
{
    public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken) => 
        await userService.DeleteUserAsync(request.Id);
}