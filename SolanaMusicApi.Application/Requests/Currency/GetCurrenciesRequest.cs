using MediatR;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Requests.Currency;

public record GetCurrenciesRequest : IRequest<List<CurrencyResponseDto>>;
