using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.DTO.Album;
using SolanaMusicApi.Domain.Entities.Music;

namespace SolanaMusicApi.Application.Services.AlbumService;

public interface IAlbumService : IBaseService<Album>
{
    IQueryable<Album> GetAlbums();
    Task<AlbumResponseDto> GetAlbumAsync(long id);
    Task<AlbumResponseDto> UpdateAlbumAsync(long id, AlbumRequestDto albumRequestDto);
    Task<AlbumResponseDto> CreateAlbumAsync(AlbumRequestDto albumRequestDto);
    Task<AlbumResponseDto> DeleteAlbumAsync(long id);
    Task AddToAlbumAsync(AddToAlbumDto addToAlbumDto);
    Task RemoveFromAlbumAsync(long trackId);
}
