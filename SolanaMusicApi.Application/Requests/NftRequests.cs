using MediatR;
using SolanaMusicApi.Domain.DTO.Nft.LikedNft;
using SolanaMusicApi.Domain.DTO.Nft.Nft;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;
using SolanaMusicApi.Domain.DTO.Transaction;

namespace SolanaMusicApi.Application.Requests;

public record GetNftRequest(long Id, long? UserId) : IRequest<GetNftResponseDto>;
public record GetNftCollectionsRequest(string? Type, long? UserId) : IRequest<List<NftCollectionResponseDto>>;
public record GetArtistNftCollectionsRequest(long ArtistId, string? Type, string? Name, long? UserId) : IRequest<List<NftCollectionResponseDto>>;
public record GetNftCollectionRequest(long Id, long? UserId) : IRequest<NftCollectionResponseDto>;
public record MintNftRequest(UpdateNftRequestDto UpdateNftRequest, TransactionRequestDto TransactionRequest) : IRequest<NftResponseDto>;
public record CreateNftCollectionRequest(NftCollectionRequestDto NftCollectionRequest, List<NftRequestDto> NftsRequest) 
    : IRequest<NftCollectionResponseDto>;
    
public record GetLikedNftRequest(long UserId) : IRequest<List<LikedNftResponseDto>>;
public record AddLikedNftRequest(LikedNftRequestDto LikedNftRequestDto) : IRequest;
public record DeleteLikedNftRequest(long Id, string? Type) : IRequest;
