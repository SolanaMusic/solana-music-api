using MediatR;
using Microsoft.AspNetCore.Http;
using SolanaMusicApi.Domain.DTO.File;
using SolanaMusicApi.Domain.Enums.File;

namespace SolanaMusicApi.Application.Requests;

public record ConvertImageRequest(IFormFile File, ImageFormats Format) : IRequest<byte[]>;
public record SaveFileRequest(FileRequestDto FileRequestDto) : IRequest<string>;
public record DeleteFileRequest(string FilePath) : IRequest<bool>;