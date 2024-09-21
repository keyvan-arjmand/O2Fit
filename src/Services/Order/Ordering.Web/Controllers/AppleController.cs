using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Ordering.Web.Controllers
{
    [Route("apple-app-site-association")]
    public class AppleController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AppleController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        //[Route("Apple/apple-app-site-association")]
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var v = await System.IO.File.ReadAllTextAsync(Path.Combine(_hostingEnvironment.WebRootPath, "apple-app-site-association"));
               
            return Content(v, "text/plain"
            );
        }

    }
}
