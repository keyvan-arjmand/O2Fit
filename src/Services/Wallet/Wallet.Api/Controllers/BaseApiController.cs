﻿using Microsoft.AspNetCore.Mvc;

namespace Wallet.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController : ControllerBase
{
}