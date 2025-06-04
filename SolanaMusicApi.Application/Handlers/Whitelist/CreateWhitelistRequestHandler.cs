using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.WhitelistService;
using WhitelistEntity = SolanaMusicApi.Domain.Entities.General.Whitelist;

namespace SolanaMusicApi.Application.Handlers.Whitelist;

public class CreateWhitelistRequestHandler(IWhitelistService whitelistService, IMapper mapper) 
    : IRequestHandler<CreateWhitelistRequest, WhitelistEntity>
{
    public async Task<WhitelistEntity> Handle(CreateWhitelistRequest request, CancellationToken cancellationToken)
    {
        var whitelist = mapper.Map<WhitelistEntity>(request.WhitelistRequestDto);
        return await whitelistService.AddAsync(whitelist);
    }
}