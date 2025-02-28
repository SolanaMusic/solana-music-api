using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolanaMusicApi.Application.Requests.File;
using SolanaMusicApi.Domain.DTO.File;
using SolanaMusicApi.Domain.Enums.File;
using System.ComponentModel.DataAnnotations;

namespace solana_music_api.Controllers;

[Route("api/files")]
[ApiController]
public class FilesController(IMediator mediator) : ControllerBase
{
    [HttpPost("convert/image")]
    public async Task<IActionResult> ConvertImage([Required] IFormFile file, [Required] ImageFormats format)
    {
        var response = await mediator.Send(new ConvertImageRequest(file, format));
        return File(response, $"image/{format.ToString().ToLower()}");
    }

    [HttpPost]
    public async Task<IActionResult> SaveFile(FileRequestDto fileRequestDto)
    {
        var response = await mediator.Send(new SaveFileRequest(fileRequestDto));
        return Ok(response);
    }

    [HttpDelete("{filePath}")]
    public async Task<IActionResult> DeleteFile(string filePath)
    {
        var response = await mediator.Send(new DeleteFileRequest(filePath));
        return Ok(response);
    }
}
