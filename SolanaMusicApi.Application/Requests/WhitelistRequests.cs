using MediatR;
using SolanaMusicApi.Domain.DTO.Whitelist;
using SolanaMusicApi.Domain.Entities.General;

namespace SolanaMusicApi.Application.Requests;

public record GetWhitelistsRequest : IRequest<List<Whitelist>>;
public record GetWhitelistRequest(long Id) : IRequest<Whitelist>;
public record CreateWhitelistRequest(WhitelistRequestDto WhitelistRequestDto) : IRequest<Whitelist>;
public record UpdateWhitelistRequest(long Id, WhitelistRequestDto WhitelistRequestDto) : IRequest<Whitelist>;
public record DeleteWhitelistRequest(long Id) : IRequest;