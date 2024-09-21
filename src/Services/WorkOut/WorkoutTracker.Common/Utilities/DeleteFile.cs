using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkoutTracker.Common.Utilities
{
    public  class DeleteFile
    {
        private readonly IWebHostEnvironment _environment;
        public DeleteFile(IWebHostEnvironment environment)
        {
            _environment = environment;
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
