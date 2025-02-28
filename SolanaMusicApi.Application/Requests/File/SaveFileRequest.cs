using MediatR;
using SolanaMusicApi.Domain.DTO.File;

namespace SolanaMusicApi.Application.Requests.File;

public record SaveFileRequest(FileRequestDto FileRequestDto) : IRequest<string>;
