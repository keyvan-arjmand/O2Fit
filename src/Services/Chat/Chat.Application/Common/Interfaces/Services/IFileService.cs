using Microsoft.AspNetCore.Http;

namespace Chat.Application.Common.Interfaces.Services;

public interface IFileService
{
    string AddImage(string imageFile, string path, string imageName);
    void RemoveImage(string imageName, string path);
    Task<string> GetImageAsBase64Async(string path);
    Task<string> AddFileAsync(string filePath, IFormFile file, CancellationToken cancellationToken = default(CancellationToken));
    void RemoveFile(string fileName, string path);

}