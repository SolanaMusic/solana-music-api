using SolanaMusicApi.Domain.Entities.Nft;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.NftRepositories.NftCollectionRepository;

public class NftCollectionRepository(ApplicationDbContext context) : BaseRepository<NftCollection>(context), INftCollectionRepository;