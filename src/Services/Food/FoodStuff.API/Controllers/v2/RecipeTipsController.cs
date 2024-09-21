using System;
using AutoMapper;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Command.RecipeSteps;
using FoodStuff.Service.v1.Query.RecipeSteps;
using FoodStuff.Service.v2.Command.RecipeSteps;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.v1.Command.RecipeTips;
using Service.v1.Command;
using WebFramework.Api;
using FoodStuff.Service.v1.Query.RecipeTips;
using FoodStuff.API.Models;
using GetByFoodIdQuery = FoodStuff.Service.v1.Query.RecipeTips.GetByFoodIdQuery;

namespace FoodStuff.API.Controllers.v2
{
    [ApiVersion("2")]
    [Authorize(Roles = "Admin")]
    public class RecipeTipsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRepository<Domain.Entities.Food.Tip> _repository;

        public RecipeTipsController(IMediator mediator, IMapper mapper, IRepository<Domain.Entities.Food.Tip> repository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SoftDeleteRecipeTipsCommand
            {
                Id = id
            }, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(PostTipDto model, CancellationToken cancellationToken)
        {
            CreateRecipeTipCommand command = new CreateRecipeTipCommand
            {
                FoodId = model.FoodId,
                TipCreate = new List<Translation>()
            };
            foreach (var i in model.Tips)
            {
                var tipName = await _mediator.Send(new CreateTranslationCommand
                {
                    Translation = _mapper.Map<CreateTranslationDto, Translation>(i.Translation)
                }, cancellationToken);

                command.TipCreate.Add(new Translation { Id = tipName.Id });
            }

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put(PutTipDto model, CancellationToken cancellationToken)
        {
            foreach (var i in model.Tips)
            {
               var a = await _mediator.Send(new UpdateTranslationCommand()
                {
                    Translation = _mapper.Map<TranslationResultDto, Translation>(i.Translation)
                }, cancellationToken);
            }
            await _mediator.Send(new UpdateRecipeTipCommand
            {
                FoodId = model.FoodId,
                Tips = model.Tips
            }, cancellationToken);
            return Ok();


        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<TipDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllRecipeTipQuery(), cancellationToken);
        }
        [HttpGet("GetById/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<TipDto> GetById(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetByIdRecipeTipQuery
            {
                Id = id,
            }, cancellationToken);
        }
        [HttpGet("GetByFoodId/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<List<TipDto>> GetByFoodId(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetByFoodIdQuery
            {
                Id = id,
            }, cancellationToken);
        }
    }
}
