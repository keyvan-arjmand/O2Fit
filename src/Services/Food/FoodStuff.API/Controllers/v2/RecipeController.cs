using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using FoodStuff.API.Models;
using FoodStuff.API.Models.DTOs;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.Models;
using FoodStuff.Service.v2.Command.Recipes;
using FoodStuff.Service.v2.Query.Recipe;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v2
{
    [ApiVersion("2")]
    [Authorize(Roles = "Admin")]
    public class RecipeController : BaseController
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("ChangeStatus")]
        public async Task<ApiResult> ChangeStatus(ChangeRecipeStatusDto dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ChangeRecipeStatusCommand
            {
                Id = dto.Id,
                Status = dto.Status
            }, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteRecipeCommand
            {
                Id = id
            }, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        [HttpGet]
        public async Task<List<RecipeGetAllDto>> GetAllPaging(int? page, int pageSize,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllRecipeQuery
            {
                Page = page,
                PageSize = pageSize
            }, cancellationToken);

        }

        [HttpGet("GetById")]
        public async Task<GetFullRecipeById> GetById(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFullRecipeByIdQuery
            {
                Id = id
            }, cancellationToken);

        }
        [HttpGet("SearchFoodByFilter")]
        [Authorize(Roles = "Admin")]
        public async Task<List<GetFullRecipeById>> SearchFoodByFilter(int page, int pageSize, [FromQuery] SearchFoodByFilterDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFoodByFilterQuery
            {
                FoodCode = request.FoodCode,
                Id = request.Id,
                PersianName = request.PersianName,
                Page = page,
                PageSize = pageSize,
                RecipeStatus = request.RecipeStatus,
            }, cancellationToken);

        }

    }
}
