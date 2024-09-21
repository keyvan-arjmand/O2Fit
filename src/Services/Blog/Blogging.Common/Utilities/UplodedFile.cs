using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Blogging.Common.Utilities
{
    public class UplodedFile
    {
        private readonly IWebHostEnvironment _environment;

        public UplodedFile(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> InsertFiles(IFormFile Filename, string FilePath)
        {
            string newFileName = string.Empty;


            newFileName = Guid.NewGuid().ToString() + Path.GetExtension(Filename.FileName);

            string ImagePath = Path.Combine(_environment.WebRootPath, FilePath) + $@"\{newFileName}";
            using (var Stream = new FileStream(ImagePath, FileMode.Create))
            {
                await Filename.CopyToAsync(Stream);
            }

            return newFileName;
        }


        public async Task<string> jsonInsertFiles(IFormFile file, string filepath)
        {
            var FolderName = Path.Combine("wwwroot", filepath);
            var PathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);

            var newFileName = "";
            newFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').ToLower();
            var fullPath = Path.Combine(PathToSave, newFileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
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

        public string UploadImage(IFormFile file, string patch)
        {
            if (!Directory.Exists(Path.Combine(_environment.WebRootPath, patch)))
            {
                Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, patch));
            }

            if (file == null) return string.Empty;
            var path = Path.Combine(_environment.WebRootPath, patch) + $@"/{file.FileName}";
            using var f = System.IO.File.Create(path);
            file.CopyTo(f);
            return file.FileName;
        }

        public void DeleteImage(string fileName, string patch)
        {
            var path = _environment.WebRootPath + "\\" + patch + "\\" + fileName;
            if (System.IO.File.Exists(path))
            {
                {
                    System.IO.File.Delete(path);
                }
            }
        }



    }
}
