using MediatR;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Requests.Currency;

public record DeleteCurrencyRequest(long Id) : IRequest<CurrencyResponseDto>;
