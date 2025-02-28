using MediatR;
using SolanaMusicApi.Application.Requests.File;
using SolanaMusicApi.Application.Services.FileService;

namespace SolanaMusicApi.Application.Handlers.File;

public class ConvertImageRequestHandler(IFileService fileService) : IRequestHandler<ConvertImageRequest, byte[]>
{
    public async Task<byte[]> Handle(ConvertImageRequest request, CancellationToken cancellationToken)
    {
        return await fileService.ConvertImageAsync(request.File, request.Format);
    }
}
