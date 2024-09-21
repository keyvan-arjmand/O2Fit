using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.Models;
using FoodStuff.Service.v2.Command.RecipeCategory;
using FoodStuff.Service.v2.Query.RecipeCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NRediSearch.QueryBuilder;
using Service.v1.Command;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v2
{
    [ApiVersion("2")]
    public class RecipeCategoryController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RecipeCategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ApiResult<List<RecipeCategoryDto>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllRecipeCategoriesQuery(), cancellationToken);
            return new ApiResult<List<RecipeCategoryDto>>(true, ApiResultStatusCode.Success, result);
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("GetAllActive")]
        public async Task<ApiResult<List<RecipeCategoryDto>>> GetAllActive(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllActiveRecipeCategoryQuery(), cancellationToken);
            return new ApiResult<List<RecipeCategoryDto>>(true, ApiResultStatusCode.Success, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}", Name = "GetRecipeCategoryById")]
        public async Task<ApiResult<RecipeCategoryDto>> GetRecipeCategoryById(int id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRecipeCategoryByIdQuery { Id = id }, cancellationToken);
            return new ApiResult<RecipeCategoryDto>(true, ApiResultStatusCode.Success, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult> Post(CreateRecipeCategoryDto dto, CancellationToken cancellationToken)
        {

            var translation = await _mediator.Send(new CreateTranslationCommand
            {
                Translation = _mapper.Map<CreateTranslationDto, Translation>(dto.Translation)
            }, cancellationToken);


            await _mediator.Send(new CreateRecipeCategoryCommand
            {
                RecipeCategory = dto,
                TranslationId = translation.Id
            }, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult> Put(UpdateRecipeCategoryDto dto, CancellationToken cancellationToken)
        {

            var translation = await _mediator.Send(new CreateTranslationCommand
            {
                Translation = _mapper.Map<CreateTranslationDto, Translation>(dto.Translation)
            }, cancellationToken);

            await _mediator.Send(new UpdateRecipeCategoryCommand
            {
                UpdateRecipeCategoryDto = dto,
                TranslationId = translation.Id
            }, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteRecipeCategoryCommand
            {
                Id = id
            }, cancellationToken);
            return new ApiResult(true, ApiResultStatusCode.Success);
        }
    }
}
