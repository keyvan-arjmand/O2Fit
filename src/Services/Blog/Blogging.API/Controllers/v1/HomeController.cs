using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common;
using Lexical.Localization;
using Lexical.Localization.Asset;
using Lexical.Localization.StringFormat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.API.Controllers.v1
{
    [ApiVersion("1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }

        [HttpGet("GetTranslation")]
        [AllowAnonymous]
        public IActionResult GetTranslation()
        {
            //IEnumerable<ILine> key_lines = LineReaderMap.Default.ReadLines(
            //       filename: "localization.ini",
            //       throwIfNotFound: true);

            IAsset asset = LineReaderMap.Default.FileAsset(
                 filename: "localization.ini",
                 throwIfNotFound: true);

            //IEnumerable<ILine> lines = IniLinesWriter.Default.ReadLines("localization.ini");


            ILine key = new LineRoot(asset).Key("LowFlame");

            return Ok();
        }

        [HttpGet("CheckToken")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult CheckToken()
        {
            var userName = HttpContext.User.Identity.GetUserName();
            var userId = HttpContext.User.Identity.GetUserId();
            var role = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Role);
            var language = HttpContext.User.Identity.FindFirstValue("Language");

            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }


    }
}
