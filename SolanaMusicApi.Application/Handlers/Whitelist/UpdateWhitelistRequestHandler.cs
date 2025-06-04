using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.WhitelistService;
using WhitelistEntity = SolanaMusicApi.Domain.Entities.General.Whitelist;

namespace SolanaMusicApi.Application.Handlers.Whitelist;

public class UpdateWhitelistRequestHandler(IWhitelistService whitelistService, IMapper mapper) 
    : IRequestHandler<UpdateWhitelistRequest, WhitelistEntity>
{
    public async Task<WhitelistEntity> Handle(UpdateWhitelistRequest request, CancellationToken cancellationToken)
    {
        var whitelist = mapper.Map<WhitelistEntity>(request.WhitelistRequestDto);
        return await whitelistService.UpdateAsync(request.Id, whitelist);
    }
}