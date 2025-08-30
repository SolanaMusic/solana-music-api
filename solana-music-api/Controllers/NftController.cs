using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Domain.DTO.Nft.Nft;
using SolanaMusicApi.Domain.DTO.Nft.NftCollection;

namespace solana_music_api.Controllers;

[Route("api/nfts")]
[ApiController]
public class NftController(IMediator mediator)
    : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNft(long id)
    {
        var response = await mediator.Send(new GetNftRequest(id));
        return Ok(response);
    }

    [HttpGet("collections")]
    public async Task<IActionResult> GetNftCollections(string? type)
    {
        var response = await mediator.Send(new GetNftCollectionsRequest(type));
        return Ok(response);
    }
    
    [HttpGet("artist-collections/{artistId}")]
    public async Task<IActionResult> GetArtistNftCollections(long artistId, string? type, string? name)
    {
        var response = await mediator.Send(new GetArtistNftCollectionsRequest(artistId, type, name));
        return Ok(response);
    }

    [HttpGet("collections/{id}")]
    public async Task<IActionResult> GetNftCollection(long id)
    {
        var response = await mediator.Send(new GetNftCollectionRequest(id));
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
}