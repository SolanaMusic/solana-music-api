using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.NftService;
using SolanaMusicApi.Domain.DTO.Nft.Nft;

namespace SolanaMusicApi.Application.Handlers.Nft;

public class GetNftRequestHandler(INftService nftService, IMapper mapper) : IRequestHandler<GetNftRequest, GetNftResponseDto>
{
    public async Task<GetNftResponseDto> Handle(GetNftRequest request, CancellationToken cancellationToken)
    {
        var response = await nftService.GetNftAsyncById(request.Id);
        return mapper.Map<GetNftResponseDto>(response);
    }
}