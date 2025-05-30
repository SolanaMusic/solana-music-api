﻿using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.TrackServices.TracksService;
using SolanaMusicApi.Domain.DTO.Track;

namespace SolanaMusicApi.Application.Handlers.Music;

public class DeleteTrackRequestHandler(ITracksService tracksService, IMapper mapper) : IRequestHandler<DeleteTrackRequest, TrackResponseDto>
{
    public async Task<TrackResponseDto> Handle(DeleteTrackRequest request, CancellationToken cancellationToken)
    {
        var response = await tracksService.DeleteTrackAsync(request.Id);
        return mapper.Map<TrackResponseDto>(response);
    }
}
