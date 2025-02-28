using MediatR;
using SolanaMusicApi.Application.Requests.Country;
using SolanaMusicApi.Application.Services.CountryService;

namespace SolanaMusicApi.Application.Handlers.Country;

public class DeleteCountryRequestHandler(ICountryService countryService) : IRequestHandler<DeleteCountryRequest, bool>
{
    public async Task<bool> Handle(DeleteCountryRequest request, CancellationToken cancellationToken)
    {
        return await countryService.DeleteAsync(request.Id);
    }
}
