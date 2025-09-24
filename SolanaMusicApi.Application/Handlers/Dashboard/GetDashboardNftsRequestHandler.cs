using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Extensions;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.NftServices.NftCollectionService;
using SolanaMusicApi.Domain.DTO.Dashboard;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;
using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.Entities.Nft;

namespace SolanaMusicApi.Application.Handlers.Dashboard;

public class GetDashboardNftsRequestHandler(INftCollectionService nftCollectionService, IMapper mapper) 
    : IRequestHandler<GetDashboardNftsRequest, PaginationResponseDto<NftCollectionResponseDto>>
{
    public Task<PaginationResponseDto<NftCollectionResponseDto>> Handle(GetDashboardNftsRequest request, CancellationToken cancellationToken)
    {
        var collections = nftCollectionService.GetNftCollections(request.Type, 0);
        collections = collections.ApplySorting(request.Sorting);
        
        if (!string.IsNullOrEmpty(request.Filter.Query))
        {
            collections = collections.Where(x => 
                EF.Functions.Like(x.Name, $"%{request.Filter.Query}%")
                || x.Nfts.Any(n => EF.Functions.Like(n.Name, $"%{request.Filter.Query}%"))
            );
        }
        
        var paginated = new DashboardResponsePaginationDto<NftCollection>(request.Filter, collections, x => x.CreatedDate);
        var response = mapper.Map<PaginationResponseDto<NftCollectionResponseDto>>(paginated);
        return Task.FromResult(response);
    }
}