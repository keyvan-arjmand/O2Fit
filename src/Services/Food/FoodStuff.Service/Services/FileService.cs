using FoodStuff.Service.Contracts;
using System;
using System.IO;

namespace FoodStuff.Service.Services
{
    public class FileService : IFileService
    {
        public string AddImage(string imageFile, string path2, string imageName)
        {
            if (string.IsNullOrEmpty(imageFile))
                return "";

            var base64EncodedBytes = Convert.FromBase64String(imageFile);

            string path = Path.Combine("wwwroot", path2);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = imageName + ".jpg";

            var address = Path.Combine(path, fileName);

            File.WriteAllBytes(address, base64EncodedBytes);

            return fileName;

        }

        public void RemoveImage(string imageName, string path2)
        {
            var pathCombine = Path.Combine("wwwroot", path2, imageName);

            if (imageName != "noimage.jpg" && imageName != "DefaultIngPic.png" && imageName != "o2fit image.jpg")
            {
                if (File.Exists(pathCombine))
                {
                    File.Delete(pathCombine);
                }
            }

        }
    }
}
