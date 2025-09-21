using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistService;
using SolanaMusicApi.Application.Services.BaseService;
using SolanaMusicApi.Application.Services.UserServices.UserService;
using SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;
using SolanaMusicApi.Domain.Entities.Performer;
using SolanaMusicApi.Domain.Enums;
using SolanaMusicApi.Infrastructure.Repositories.BaseRepository;

namespace SolanaMusicApi.Application.Services.ArtistServices.ArtistApplicationService;

public class ArtistApplicationService(IBaseRepository<ArtistApplication> baseRepository, IUserService userService, IArtistService artistService)
    : BaseService<ArtistApplication>(baseRepository), IArtistApplicationService
{
    public IQueryable<ArtistApplication> GetUsersApplications()
    {
        return GetAll()
            .Include(x => x.User)
                .ThenInclude(x => x.Profile)
                    .ThenInclude(x => x.Country)
            .Include(x => x.Reviewer);
    }

    public async Task<ArtistApplication?> GetUserApplicationAsync(long id) =>
        await GetUsersApplications().FirstOrDefaultAsync();

    public async Task<ArtistApplication> UpdateUserApplicationAsync(long id, UpdateArtistApplicationDto updateArtistApplicationDto)
    {
        var application = await GetByIdAsync(id);

        if (updateArtistApplicationDto.ReviewerId.HasValue)
        {
            var role = await userService.GetUserRoleAsync(updateArtistApplicationDto.ReviewerId.Value);

            if (role != nameof(UserRoles.Admin) && role != nameof(UserRoles.Moderator))
                throw new MethodAccessException("You do not have permission to perform this action");

            application.ReviewerId = updateArtistApplicationDto.ReviewerId.Value;
        }

        if (!application.ReviewerId.HasValue)
            throw new Exception("Reviewer must be specified");

        if (updateArtistApplicationDto.Status.HasValue)
            application.Status = updateArtistApplicationDto.Status.Value;

        await BeginTransactionAsync();
        
        try
        {
            await CreateArtistAsync(application.UserId, updateArtistApplicationDto);
            var response = await UpdateAsync(id, application);
            await userService.UpdateUserRoleAsync(application.UserId, UserRoles.Artist);

            await CommitTransactionAsync();
            return response;
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync(ex);
            throw;
        }
    }

    private async Task CreateArtistAsync(long userId, UpdateArtistApplicationDto updateArtistApplicationDto)
    {
        if (updateArtistApplicationDto.Status != ArtistApplicationStatus.Approved)
            return;
        
        if (string.IsNullOrEmpty(updateArtistApplicationDto.Bio) 
            || string.IsNullOrEmpty(updateArtistApplicationDto.ArtistName) || !updateArtistApplicationDto.CountryId.HasValue)
        {
            throw new InvalidOperationException("You must provide artist data");
        }

        var artist = new Artist
        {
            UserId = userId,
            Name = updateArtistApplicationDto.ArtistName,
            Bio = updateArtistApplicationDto.Bio,
            CountryId = updateArtistApplicationDto.CountryId.Value
        };

        await artistService.CreateArtistAsync(artist);
    }
}