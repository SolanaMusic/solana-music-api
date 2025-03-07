using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Currency;
using SolanaMusicApi.Application.Services.CurrencyService;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Handlers.Currency;

public class GetCurrencyRequestHandler(ICurrencyService currencyService, IMapper mapper) : IRequestHandler<GetCurrencyRequest, CurrencyResponseDto>
{
    public async Task<CurrencyResponseDto> Handle(GetCurrencyRequest request, CancellationToken cancellationToken)
    {
        var response = await currencyService.GetByIdAsync(request.Id);
        return mapper.Map<CurrencyResponseDto>(response);
    }
}
