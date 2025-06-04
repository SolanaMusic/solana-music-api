using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.WhitelistService;
using WhitelistEntity = SolanaMusicApi.Domain.Entities.General.Whitelist;

namespace SolanaMusicApi.Application.Handlers.Whitelist;

public class GetWhitelistRequestHandler(IWhitelistService whitelistService) : IRequestHandler<GetWhitelistRequest, WhitelistEntity>
{
    public async Task<WhitelistEntity> Handle(GetWhitelistRequest request, CancellationToken cancellationToken) => 
        await whitelistService.GetByIdAsync(request.Id);
}