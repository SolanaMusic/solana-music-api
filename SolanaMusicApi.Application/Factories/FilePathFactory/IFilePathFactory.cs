using SolanaMusicApi.Domain.Enums.File;

namespace SolanaMusicApi.Application.Factories.FilePathFactory;

public interface IFilePathFactory
{
    (string Path, string Prefix) GetFilePath(FileTypes fileType);
}
