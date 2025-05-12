using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Handlers.Album;

public class GetAlbumsByTracksRequestHandler(IAlbumService albumService, IMapper mapper) 
    : IRequestHandler<GetAlbumsByArtistsRequest, List<AlbumResponseDto>>
{
    public async Task<List<AlbumResponseDto>> Handle(GetAlbumsByArtistsRequest request, CancellationToken cancellationToken)
    {
        var query = albumService.GetAlbums()
            .Where(x => x.ArtistAlbums.Any(aa => request.ArtistIds.Contains(aa.ArtistId)));

        if (!string.IsNullOrWhiteSpace(request.Title))
            query = query.Where(x => EF.Functions.Like(x.Title, $"%{request.Title}%"));

        var albums = await query.ToListAsync(cancellationToken);
        return mapper.Map<List<AlbumResponseDto>>(albums);
    }
}