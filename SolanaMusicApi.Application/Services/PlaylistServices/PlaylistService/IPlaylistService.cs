using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.DTO.Playlist;
using SolanaMusicApi.Domain.Entities.Playlist;

namespace SolanaMusicApi.Application.Services.PlaylistServices.PlaylistService;

public interface IPlaylistService : IBaseService<Playlist>
{
    IQueryable<Playlist> GetPlaylists(long userId);
    Task<Playlist> GetPlaylistAsync(long id);
    Task<Playlist> CreatePlaylistAsync(Playlist playlist, CreatePlaylistRequestDto playlistRequestDto);
    Task<Playlist> UpdatePlaylistAsync(long id, UpdatePlaylistRequestDto playlistRequestDto);
    Task<Playlist> DeletePlaylistAsync(long id);
    Task AddToPlaylistAsync(AddToPlaylistDto addToPlaylistDto);
    Task RemoveFromPlaylistAsync(AddToPlaylistDto addToPlaylistDto);
}
