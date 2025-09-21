using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;
using SolanaMusicApi.Domain.Entities.Performer;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistApplicationService;

public interface IArtistApplicationService : IBaseService<ArtistApplication>
{
    IQueryable<ArtistApplication> GetUsersApplications();
    Task<ArtistApplication> UpdateUserApplicationAsync(long id, UpdateArtistApplicationDto updateArtistApplicationDto);
}