namespace Advertise.Application.Common.Interfaces.Services;

public interface IFileService
{
    string AddImage(string imageFile, string path, string imageName);
    void RemoveImage(string imageName, string path);
    Task<string> GetImageAsBase64Async(string path);
}