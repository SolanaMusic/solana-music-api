using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.Entities.Nft;

namespace SolanaMusicApi.Application.Services.NftServices.NftService;

public interface INftService : IBaseService<Nft>
{
    Task<Nft> GetNftAsyncById(long id);
}