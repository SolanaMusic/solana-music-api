using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistApplicationService;

namespace SolanaMusicApi.Application.Handlers.Artist.ArtistApplication;

public class CreateArtistApplicationRequestHandler(IArtistApplicationService artistApplicationService, IMapper mapper) 
    : IRequestHandler<CreateArtistApplicationRequest, Domain.Entities.Performer.ArtistApplication>
{
    public async Task<Domain.Entities.Performer.ArtistApplication> Handle(CreateArtistApplicationRequest request, CancellationToken cancellationToken)
    {
        var application = mapper.Map<Domain.Entities.Performer.ArtistApplication>(request.ApplicationRequestDto);
       return await artistApplicationService.AddAsync(application);
    }
}