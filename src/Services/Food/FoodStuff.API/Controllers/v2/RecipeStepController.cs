using System.Collections.Generic;
using System.Linq;
using FoodStuff.Service.v2.Command.Recipes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Common;
using FoodStuff.Domain.Entities.Translation;
using MediatR;
using WebFramework.Api;
using FoodStuff.Service.v2.Command.RecipeSteps;
using Microsoft.AspNetCore.Authorization;
using FoodStuff.Service.Models;
using Service.v1.Command;
using AutoMapper;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Service.v1.Command.RecipeSteps;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.v1.Query.RecipeSteps;
using Microsoft.EntityFrameworkCore;
using FoodStuff.Service.v1.Query.RecipeTips;
using GetByFoodIdQuery = FoodStuff.Service.v1.Query.RecipeSteps.GetByFoodIdQuery;

namespace FoodStuff.API.Controllers.v2
{
    [ApiVersion("2")]
    [Authorize(Roles = "Admin")]
    public class RecipeStepController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRepository<Domain.Entities.Food.RecipeStep> _repository;
        private readonly IRepository<Domain.Entities.Food.Recipe> _repositoryrecipe;
        public RecipeStepController(IMediator mediator, IMapper mapper, IRepository<Domain.Entities.Food.RecipeStep> repository, IRepository<Recipe> repositoryrecipe)
        {
            _mediator = mediator;
            _mapper = mapper;
            _repository = repository;
            _repositoryrecipe = repositoryrecipe;
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SoftDeleteRecipeStepCommand
            {
                Id = id
            }, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(PostRecipeStepDto model, CancellationToken cancellationToken)
        {
            CreateRecipeStepsCommand command = new CreateRecipeStepsCommand
            {
                FoodId = model.FoodId,
                StepCreate = new List<Translation>()
            };
            foreach (var recipeStep in model.RecipeSteps)
            {
                var stepName = await _mediator.Send(new CreateTranslationCommand
                {
                    Translation = _mapper.Map<CreateTranslationDto, Translation>(recipeStep.Translation)
                }, cancellationToken);
                command.StepCreate.Add(new Translation { Id = stepName.Id });
            }

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put(PutRecipeStepDto model, CancellationToken cancellationToken)
        {

            foreach (var translation in model.RecipeSteps.Select(x => x.Translation).ToList())
            {
                await _mediator.Send(new UpdateTranslationCommand()
                {
                    Translation = _mapper.Map<TranslationResultDto, Translation>(translation)
                }, cancellationToken);
            }
            await _mediator.Send(new UpdateRangeRecipeStepCommand
            {
                RecipeSteps = model.RecipeSteps,
                FoodId = model.FoodId
            }, cancellationToken);
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<RecipeStepDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllRecipeQuery(), cancellationToken);
        }
        [HttpGet("GetById/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<RecipeStepDto> GetById(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetByIdRecipeQuery
            {
                Id = id,
            }, cancellationToken);
        }

        [HttpGet("GetByFoodId/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<List<RecipeStepDto>> GetByFoodId(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetByFoodIdQuery
            {
                Id = id,
            }, cancellationToken);
        }

    }
}
