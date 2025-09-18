using SolanaMusicApi.Domain.Entities.Nft;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Infrastructure.Repositories.NftRepositories.LikedNftRepository;

public class LikedNftRepository(ApplicationDbContext context) : BaseRepository<LikedNft>(context), ILikedNftRepository;