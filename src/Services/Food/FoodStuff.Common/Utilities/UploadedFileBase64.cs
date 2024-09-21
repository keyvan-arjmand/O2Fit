using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FoodStuff.Common.Utilities
{
   public class UploadedFileBase64
    {
        private readonly IWebHostEnvironment _environment;

        public UploadedFileBase64(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> InsertFiles(string Filename, string FilePath)
        {
            var base64EncodedBytes = Convert.FromBase64String(Filename);

            string _Path =Path.Combine(_environment.WebRootPath, FilePath);

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
    }
}
       