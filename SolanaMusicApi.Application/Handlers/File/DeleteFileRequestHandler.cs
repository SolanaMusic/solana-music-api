using MediatR;
using SolanaMusicApi.Application.Requests.File;
using SolanaMusicApi.Application.Services.FileService;

namespace SolanaMusicApi.Application.Handlers.File;

public class DeleteFileRequestHandler(IFileService fileService) : IRequestHandler<DeleteFileRequest, bool>
{
    public Task<bool> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(fileService.DeleteFile(request.FilePath));
    }
}
