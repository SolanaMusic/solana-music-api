using MediatR;
using SolanaMusicApi.Domain.DTO.Currency;

namespace SolanaMusicApi.Application.Requests;

public record GetCurrenciesRequest : IRequest<List<CurrencyResponseDto>>;
public record GetCurrencyRequest(long Id) : IRequest<CurrencyResponseDto>;
public record CreateCurrencyRequest(CurrencyRequestDto CurrencyRequestDto) : IRequest<CurrencyResponseDto>;
public record UpdateCurrencyRequest(long Id, CurrencyRequestDto CurrencyRequestDto) : IRequest<CurrencyResponseDto>;
public record DeleteCurrencyRequest(long Id) : IRequest<CurrencyResponseDto>;
