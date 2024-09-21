using System;
using System.IO;
using System.Threading.Tasks;

namespace FoodStuff.Service.Services
{
    public static class ConvertImage
    {
        public static async Task<string> GetImageAsBase64Url(string path)
        {
            try
            {
                byte[] imageArray = await File.ReadAllBytesAsync($"wwwroot/{path}");
                return Convert.ToBase64String(imageArray);
            }
            catch
            {
                return "";
            }

        }
    }
}
