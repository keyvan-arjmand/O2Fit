using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.v1.Command.DeleteAllFoodRedis;
using FoodStuff.Service.v1.Query.RedisSpeed;
using Lexical.Localization;
using Lexical.Localization.Asset;
using Lexical.Localization.StringFormat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class FastFoodController : BaseController
    {
        private readonly IAsset _asset;
        private readonly IMediator _mediator;

        public FastFoodController(IAsset asset, IMediator mediator)
        {
            _asset = asset;
            _mediator = mediator;
        }

        [HttpGet("Test")]
        public async Task<ActionResult> Test()
        {
            ILine line = new LineRoot().Assembly("FoodStuff.Domain").Type("FoodStuff.Domain.Enum.BakingType").Key("HighFlame").Culture("fa");
            IString _value = _asset.GetLine(line).GetString();

            return Ok(_value.Text);
        }

        [HttpGet("RedisSpeed")]
        public async Task<IActionResult> RedisSpeed()
        {
            return Ok(await _mediator.Send(new GetRedisSpeedQuery()));
        }

        [HttpPost("DeleteFoodRedis")]
        public async Task<IActionResult> DeleteFoodRedis()
        {
            return Ok(await _mediator.Send(new DeleteAllFoodRedisCommand()));
        }
    }
}
