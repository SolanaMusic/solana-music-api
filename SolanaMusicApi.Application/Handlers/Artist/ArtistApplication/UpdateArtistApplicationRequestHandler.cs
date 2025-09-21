using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistApplicationService;
using SolanaMusicApi.Domain.DTO.Artist.ArtistApplication;

namespace SolanaMusicApi.Application.Handlers.Artist.ArtistApplication;

public class UpdateArtistApplicationRequestHandler(IArtistApplicationService artistApplicationService, IMapper mapper)
    : IRequestHandler<UpdateArtistApplicationRequest, ArtistApplicationResponseDto>
{
    public async Task<ArtistApplicationResponseDto> Handle(UpdateArtistApplicationRequest request, CancellationToken cancellationToken)
    {
        var response = await artistApplicationService
            .UpdateUserApplicationAsync(request.Id, request.UpdateArtistApplicationDto);
        
        return mapper.Map<ArtistApplicationResponseDto>(response);
    }
}