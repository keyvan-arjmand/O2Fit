using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Blogging.API.Data
{
    public class Upload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Upload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Uploadfile(IFormFile file, string patch)
        {
            if (file == null) return string.Empty;
            var path = _webHostEnvironment.WebRootPath + "\\" + patch + "\\" + file.FileName;
            using var f = System.IO.File.Create(path);
            file.CopyTo(f);
            return file.FileName;
        }
    }
}