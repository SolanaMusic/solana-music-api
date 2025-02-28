using MediatR;

namespace SolanaMusicApi.Application.Requests.File;

public record DeleteFileRequest(string FilePath) : IRequest<bool>;
