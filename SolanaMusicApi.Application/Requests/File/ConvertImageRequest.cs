using MediatR;
using Microsoft.AspNetCore.Http;
using SolanaMusicApi.Domain.Enums.File;

namespace SolanaMusicApi.Application.Requests.File;

public record ConvertImageRequest(IFormFile File, ImageFormats Format) : IRequest<byte[]>;
