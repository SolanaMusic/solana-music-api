using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.WhitelistService;

namespace SolanaMusicApi.Application.Handlers.Whitelist;

public class DeleteWhitelistRequestHandler(IWhitelistService whitelistService) : IRequestHandler<DeleteWhitelistRequest>
{
    public async Task Handle(DeleteWhitelistRequest request, CancellationToken cancellationToken) => 
        await whitelistService.DeleteAsync(request.Id);
}