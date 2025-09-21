using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.ArtistServices.ArtistApplicationService;
using SolanaMusicApi.Domain.Enums;

namespace SolanaMusicApi.Application.Handlers.Dashboard;

public class GetPendingApplicationsHandler(IArtistApplicationService artistApplicationService) 
    : IRequestHandler<GetPendingApplications, int>
{
    public async Task<int> Handle(GetPendingApplications request, CancellationToken cancellationToken)
    {
        return await artistApplicationService.GetAll()
            .CountAsync(x => x.Status == ArtistApplicationStatus.Pending, cancellationToken);
    }
}