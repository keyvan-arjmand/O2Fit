namespace Nutritionist.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController : ControllerBase
{
    // protected string LanguageName =>
   //     HttpContext.Request.Headers["Language"]!;
}