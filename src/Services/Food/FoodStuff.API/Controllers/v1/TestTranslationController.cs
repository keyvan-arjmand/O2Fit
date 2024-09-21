using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodStuff.Domain.Entities.Translation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using Service.v1.Command;
using Service.v1.Query;
using AutoMapper;
using FoodStuff.API.Models;
using Service.DataInitializer;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Enum;
using FoodStuff.Common.Utilities;
using Common.Utilities;
using Microsoft.OpenApi.Extensions;
using FoodStuff.Service.v1.Query;

namespace FoodStuff.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class TestTranslationController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TestTranslationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ApiResult<Translation>> Get(int id)
        {
            var _translation = await _mediator.Send(new GetTranslationByIdQuery { Id = id });
            return Ok(_translation);
        }

        [HttpGet("GetCalIngredient")]
        public async Task<ApiResult<SelectIngredient>> GetCalIngredient()
        {
            var _cal = await _mediator.Send(new GetIngredientCalculateQuery { IngredientModels = IngredientDataInitializer.MockData() });
            return Ok(_cal);
        }
        
        [HttpPost]
        public async Task<ApiResult<Translation>> Post(TranslationDto translation)
        {
            try
            {
                var trans = await _mediator.Send(new CreateTranslationCommand
                {
                    Translation = translation.ToEntity(_mapper)
                });

                return Ok(trans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ApiResult<Translation>> Put(TranslationDto translation)
        {
            try
            {
                return await _mediator.Send(new UpdateTranslationCommand
                {
                    Translation = translation.ToEntity(_mapper)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
