using MediatR;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Requests.Currency;

public record CreateCurrencyRequest(CurrencyRequestDto CurrencyRequestDto) : IRequest<CurrencyResponseDto>;
