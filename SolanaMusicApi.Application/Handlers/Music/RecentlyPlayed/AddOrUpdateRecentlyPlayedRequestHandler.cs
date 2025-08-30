using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.RecentlyPlayedService;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Track.RecentlyPlayed;

namespace SolanaMusicApi.Application.Handlers.Music.RecentlyPlayed;

public class AddOrUpdateRecentlyPlayedRequestHandler(IRecentlyPlayedService recentlyPlayedService, ITracksService tracksService, IMapper mapper) 
    : IRequestHandler<AddOrUpdateRecentlyPlayedRequest, RecentlyPlayedResponseDto>
{
    public async Task<RecentlyPlayedResponseDto> Handle(AddOrUpdateRecentlyPlayedRequest request, CancellationToken cancellationToken)
    {
        await recentlyPlayedService.BeginTransactionAsync();

        try
        {
            var record = await recentlyPlayedService
                .GetAll()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.TrackId == request.TrackId, cancellationToken);

            var track = await tracksService.GetByIdAsync(request.TrackId);
            track.PlaysCount += 1;
            await tracksService.UpdateAsync(track);
        
            if (record == null)
            {
                var recentlyPlayed = mapper.Map<Domain.Entities.General.RecentlyPlayed>(request);
                var added = await recentlyPlayedService.AddAsync(recentlyPlayed);
                
                await recentlyPlayedService.CommitTransactionAsync();
                return mapper.Map<RecentlyPlayedResponseDto>(added);
            }
        
            record.UpdatedDate = DateTime.UtcNow;
            var updated = await recentlyPlayedService.UpdateAsync(record);
            
            await recentlyPlayedService.CommitTransactionAsync();
            return mapper.Map<RecentlyPlayedResponseDto>(updated);
        }
        catch (Exception ex)
        {
            await recentlyPlayedService.RollbackTransactionAsync(ex);
            throw;
        }
    }
}