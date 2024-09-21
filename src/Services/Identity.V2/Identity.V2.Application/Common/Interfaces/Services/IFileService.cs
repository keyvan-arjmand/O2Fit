namespace Identity.V2.Application.Common.Interfaces.Services;

public interface IFileService
{ 
    string AddImage(string imageFile, string path2, string imageName);
    void RemoveImage(string imageName, string path2);
}