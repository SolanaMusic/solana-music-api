using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.WhitelistService;
using WhitelistEntity = SolanaMusicApi.Domain.Entities.General.Whitelist;

namespace SolanaMusicApi.Application.Handlers.Whitelist;

public class GetWhitelistsRequestHandler(IWhitelistService whitelistService) 
    : IRequestHandler<GetWhitelistsRequest, List<WhitelistEntity>>
{
    public async Task<List<WhitelistEntity>> Handle(GetWhitelistsRequest request, CancellationToken cancellationToken) => 
        await whitelistService.GetAll().ToListAsync(cancellationToken: cancellationToken);
}