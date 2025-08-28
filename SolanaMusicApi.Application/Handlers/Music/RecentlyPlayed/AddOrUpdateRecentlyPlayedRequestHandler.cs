using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.RecentlyPlayedService;
using SolanaMusicApi.Domain.DTO.Track.RecentlyPlayed;

namespace SolanaMusicApi.Application.Handlers.Music.RecentlyPlayed;

public class AddOrUpdateRecentlyPlayedRequestHandler(IRecentlyPlayedService recentlyPlayedService, IMapper mapper) 
    : IRequestHandler<AddOrUpdateRecentlyPlayedRequest, RecentlyPlayedResponseDto>
{
    public async Task<RecentlyPlayedResponseDto> Handle(AddOrUpdateRecentlyPlayedRequest request, CancellationToken cancellationToken)
    {
        var record = await recentlyPlayedService
            .GetAll()
            .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.TrackId == request.TrackId, cancellationToken);

        if (record == null)
        {
            var recentlyPlayed = mapper.Map<Domain.Entities.General.RecentlyPlayed>(request);
            var added = await recentlyPlayedService.AddAsync(recentlyPlayed);
            return mapper.Map<RecentlyPlayedResponseDto>(added);
        }
        
        record.UpdatedDate = DateTime.UtcNow;
        var updated = await recentlyPlayedService.UpdateAsync(record);
        return mapper.Map<RecentlyPlayedResponseDto>(updated);
    }
}