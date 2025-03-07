using MediatR;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Requests.Currency;

public record GetCurrencyRequest(long Id) : IRequest<CurrencyResponseDto>;
