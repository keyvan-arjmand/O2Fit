namespace Track.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected string Language =>
            string.IsNullOrWhiteSpace(HttpContext?.Request.Headers["Language"])
                ? "Persian"
                : HttpContext?.Request.Headers["Language"]!;
    }
}