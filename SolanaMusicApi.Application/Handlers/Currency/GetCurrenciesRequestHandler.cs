using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.CurrencyService;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Handlers.Currency;

public class GetCurrenciesRequestHandler(ICurrencyService currencyService, IMapper mapper) : IRequestHandler<GetCurrenciesRequest, List<CurrencyResponseDto>>
{
    public Task<List<CurrencyResponseDto>> Handle(GetCurrenciesRequest request, CancellationToken cancellationToken)
    {
        var response = currencyService.GetAll();
        return Task.FromResult(mapper.Map<List<CurrencyResponseDto>>(response));
    }
}
