using Microsoft.AspNetCore.Http;

namespace Chat.Infrastructure.Services;

public class FileService : IFileService
{
    public string AddImage(string imageFile, string path, string imageName)
    {
        if (string.IsNullOrEmpty(imageFile))
            return "";

        var base64EncodedBytes = Convert.FromBase64String(imageFile);

        string filePath = Path.Combine("wwwroot", path);

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        string fileName = imageName + ".jpg";

        var address = Path.Combine(filePath, fileName);

        File.WriteAllBytes(address, base64EncodedBytes);

        return fileName;
    }

    public void RemoveImage(string imageName, string path)
    {
        var pathCombine = Path.Combine("wwwroot", path, imageName);

        if (imageName != "noimage.jpg" && imageName != "DefaultIngPic.png" && imageName != "o2fit image.jpg")
        {
            if (File.Exists(pathCombine))
            {
                File.Delete(pathCombine);
            }
        }
    }

    public async Task<string> GetImageAsBase64Async(string path)
    {
        var imageBytes = await File.ReadAllBytesAsync($"wwwroot/{path}");
        return Convert.ToBase64String(imageBytes);
    }

    public async Task<string> AddFileAsync(string filePath, IFormFile file,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        if (file.Length <= 0) return string.Empty;

        var folderName = Path.Combine("wwwroot", filePath);
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        CreatePath(pathToSave);
        var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(pathToSave, newFileName);
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        return newFileName;
    }

    public void RemoveFile(string fileName, string path)
    {
        var pathCombine = Path.Combine("wwwroot", path, fileName);

        if (File.Exists(pathCombine))
        {
            File.Delete(pathCombine);
        }
    }


    private void CreatePath(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}