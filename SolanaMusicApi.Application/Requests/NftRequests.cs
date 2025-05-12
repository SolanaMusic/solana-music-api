using MediatR;
using SolanaMusicApi.Domain.DTO.Nft.Nft;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;
using SolanaMusicApi.Domain.DTO.Transaction;

namespace SolanaMusicApi.Application.Requests;

public record GetNftRequest(long Id) : IRequest<GetNftResponseDto>;
public record GetNftCollectionsRequest(string? Type) : IRequest<List<NftCollectionResponseDto>>;
public record GetArtistNftCollectionsRequest(long ArtistId, string? Type, string? Name) : IRequest<List<NftCollectionResponseDto>>;
public record GetNftCollectionRequest(long Id) : IRequest<NftCollectionResponseDto>;
public record MintNftRequest(UpdateNftRequestDto UpdateNftRequest, TransactionRequestDto TransactionRequest) : IRequest<NftResponseDto>;
public record CreateNftCollectionRequest(NftCollectionRequestDto NftCollectionRequest, List<NftRequestDto> NftsRequest) 
    : IRequest<NftCollectionResponseDto>;