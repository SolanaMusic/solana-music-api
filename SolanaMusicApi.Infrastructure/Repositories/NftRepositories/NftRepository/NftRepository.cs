using SolanaMusicApi.Domain.Entities.Nft;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.NftRepositories.NftRepository;

public class NftRepository(ApplicationDbContext context) : BaseRepository<Nft>(context), INftRepository;