using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.AlbumService;
using SolanaMusicApi.Domain.DTO.Album;

namespace SolanaMusicApi.Application.Handlers.Album;

public class GetAlbumsByArtistRequestHandler(IAlbumService albumService, IMapper mapper) 
    : IRequestHandler<GetAlbumsByArtistRequest, List<AlbumResponseDto>>
{
    public async Task<List<AlbumResponseDto>> Handle(GetAlbumsByArtistRequest request, CancellationToken cancellationToken)
    {
        var query = albumService.GetAlbums()
            .Where(x => x.ArtistAlbums.Any(aa => request.ArtistIds.Contains(aa.ArtistId)));

        if (!string.IsNullOrWhiteSpace(request.Title))
            query = query.Where(x => EF.Functions.Like(x.Title, $"%{request.Title}%"));

        var response = await query.ToListAsync(cancellationToken);
        return mapper.Map<List<AlbumResponseDto>>(response);
    }
}