using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace User.Common.Utilities
{
    public class UplodedFile
    {
        private readonly IWebHostEnvironment _environment;

        public UplodedFile(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> InsertFiles(string Name, IFormFile Filename, string FilePath)
        {
            string newFileName = string.Empty;
            if (Name == null)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(Filename.FileName);
            }
            else
            {
                newFileName = Name + Path.GetExtension(Filename.FileName);
            }
            string ImagePath = Path.Combine(_environment.WebRootPath, FilePath) + $@"\{newFileName}";
            using (var Stream = new FileStream(ImagePath, FileMode.Create))
            {
                await Filename.CopyToAsync(Stream);
            }

            return newFileName;
        }

        public void DeleteFiles(string Filename, string FilePath)
        {

            string DeletePath = Path.Combine(_environment.WebRootPath, FilePath) + $@"\{Filename}";
            if (System.IO.File.Exists(DeletePath))
            {
                {
                    System.IO.File.Delete(DeletePath);
                }
            }


        }

        public string UploadUserProfile(string imageUri)
        {
            if (string.IsNullOrWhiteSpace(imageUri))
                return string.Empty;

            var base64EncodedBytes = Convert.FromBase64String(imageUri);

            string _Path = Path.Combine(_environment.WebRootPath, "UserProfile");

            DirectoryInfo destination;

            if (!Directory.Exists(_Path))
            {
                destination = Directory.CreateDirectory(_Path);
            }
            else
            {
                destination = new DirectoryInfo(_Path);
            }

            string _FileName = Guid.NewGuid().ToString() + ".jpg";

            var _Address = Path.Combine(_Path, _FileName);

            File.WriteAllBytes(_Address, base64EncodedBytes);

            return _FileName;
        }
    }
}