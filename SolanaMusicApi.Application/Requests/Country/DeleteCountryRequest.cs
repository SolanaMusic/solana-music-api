using MediatR;

namespace SolanaMusicApi.Application.Requests.Country;

public record DeleteCountryRequest(long Id) : IRequest<bool>;
