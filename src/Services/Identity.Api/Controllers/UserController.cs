using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

public class UserController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("test")]
    public string Test()
    {
        return "Ok()";
    }
}