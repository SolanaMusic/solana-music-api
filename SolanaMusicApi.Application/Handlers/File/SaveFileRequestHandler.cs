using MediatR;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.FileService;

namespace SolanaMusicApi.Application.Handlers.File;

public class SaveFileRequestHandler(IFileService fileService) : IRequestHandler<SaveFileRequest, string>
{
    public async Task<string> Handle(SaveFileRequest request, CancellationToken cancellationToken)
    {
        return await fileService.SaveFileAsync(request.FileRequestDto.File, request.FileRequestDto.FileType, request.FileRequestDto.ImageFormat);
    }
}
