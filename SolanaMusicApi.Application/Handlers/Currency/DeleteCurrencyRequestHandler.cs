using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests.Currency;
using SolanaMusicApi.Application.Services.CurrencyService;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Handlers.Currency;

public class DeleteCurrencyRequestHandler(ICurrencyService currencyService, IMapper mapper) : IRequestHandler<DeleteCurrencyRequest, CurrencyResponseDto>
{
    public async Task<CurrencyResponseDto> Handle(DeleteCurrencyRequest request, CancellationToken cancellationToken)
    {
        var response = await currencyService.DeleteAsync(request.Id);
        return mapper.Map<CurrencyResponseDto>(response);
    }
}
