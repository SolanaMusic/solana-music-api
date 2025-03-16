using AutoMapper;
using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.CurrencyService;
using SolanaMusicApi.Domain.DTO.Currency;
using CurrencyEntity = SolanaMusicApi.Domain.Entities.Transaction.Currency;

namespace SolanaMusicApi.Application.Handlers.Currency;

public class UpdateCurrencyRequestHandler(ICurrencyService currencyService, IMapper mapper) : IRequestHandler<UpdateCurrencyRequest, CurrencyResponseDto>
{
    public async Task<CurrencyResponseDto> Handle(UpdateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var currency = mapper.Map<CurrencyEntity>(request.CurrencyRequestDto);
        var response = await currencyService.UpdateAsync(request.Id, currency);
        return mapper.Map<CurrencyResponseDto>(response);
    }
}
