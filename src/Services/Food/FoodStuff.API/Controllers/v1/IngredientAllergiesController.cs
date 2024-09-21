using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Command.IngredientAllergies;
using FoodStuff.Service.v1.Query.IngredientAllergies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    [Authorize(Roles = "Admin")]
    public class IngredientAllergiesController : BaseController
    {
        private readonly IMediator _mediator;

        public IngredientAllergiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResult<PageResult<IngredientAllergyDto>>> Get(int pageIndex, int pageSize,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPaginatedIngredientAllergiesQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            }, cancellationToken).ConfigureAwait(false);

            if (result.Count == 0)
            {
                return new ApiResult<PageResult<IngredientAllergyDto>>(false, ApiResultStatusCode.BadRequest, null);
            }

            return result;
        }

        [HttpPost]
        public async Task<ApiResult> Post(CreateIngredientAllergiesDto ingredientAllergiesDto,
            CancellationToken cancellationToken)
        {
            if (ingredientAllergiesDto == null)
            {
                return new ApiResult(false, ApiResultStatusCode.BadRequest);
            }

            await _mediator.Send(new CreateIngredientAllergiesCommand
            {
                IngredientId = ingredientAllergiesDto.IngredientId,
                IsChecked = ingredientAllergiesDto.IsChecked
            }, cancellationToken).ConfigureAwait(false);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }


    }
}
