using MediatR;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Requests.Currency;

public record UpdateCurrencyRequest(long Id, CurrencyRequestDto CurrencyRequestDto) : IRequest<CurrencyResponseDto>;
