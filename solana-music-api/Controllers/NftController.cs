using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Nft.LikedNft;
using SolanaMusicApi.Domain.DTO.Nft.Nft;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;

namespace solana_music_api.Controllers;

[Route("api/nfts")]
[ApiController]
public class NftController(IMediator mediator)
    : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNft(long id, long? userId = null)
    {
        var response = await mediator.Send(new GetNftRequest(id, userId));
        return Ok(response);
    }

    [HttpGet("collections")]
    public async Task<IActionResult> GetNftCollections(string? type, long? userId = null)
    {
        var response = await mediator.Send(new GetNftCollectionsRequest(type, userId));
        return Ok(response);
    }
    
    [HttpGet("artist-collections/{artistId}")]
    public async Task<IActionResult> GetArtistNftCollections(long artistId, string? type, string? name, long? userId = null)
    {
        var response = await mediator.Send(new GetArtistNftCollectionsRequest(artistId, type, name, userId));
        return Ok(response);
    }

    [HttpGet("collections/{id}")]
    public async Task<IActionResult> GetNftCollection(long id, long? userId)
    {
        var response = await mediator.Send(new GetNftCollectionRequest(id, userId));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNftCollection(NftCollectionWithNftsRequestDto request)
    {
        var response = await mediator.Send(new CreateNftCollectionRequest(request.NftCollection, request.Nfts));
        return CreatedAtAction(nameof(GetNftCollection), new { id = response.Id }, response);
    }

    [HttpPost("mint")]
    public async Task<IActionResult> MintNft(MintNftRequestDto request)
    {
        var response = await mediator.Send(new MintNftRequest(request.NftRequest, request.TransactionRequest));
        return Ok(response);
    }

    [HttpGet("liked/{userId}")]
    public async Task<IActionResult> GetLikedNft(long userId)
    {
        var response = await mediator.Send(new GetLikedNftRequest(userId));
        return Ok(response);
    }

    [HttpPost("liked")]
    public async Task<IActionResult> AddLikedNft(LikedNftRequestDto request)
    {
        var dto = new LikedNftRequestDto
        {
            UserId = request.UserId,
            NftId = request.NftId,
            CollectionId = request.CollectionId
        };
        
        dto.Validate();
        
        await mediator.Send(new AddLikedNftRequest(dto));
        return NoContent();
    }
    
    [HttpDelete("liked/{id}")]
    public async Task<IActionResult> DeleteLikedNft(long id, string? type)
    {
        await mediator.Send(new DeleteLikedNftRequest(id, type));
        return NoContent();
    }
}