using Microsoft.AspNetCore.Mvc;
using WebFramework.Filters;

namespace WebFramework.Api
{
    [ApiController]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        protected string LanguageName =>
        HttpContext?.Request.Headers["Language"] ?? string.Empty;

        public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
    }
}
