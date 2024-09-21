using AutoMapper;
using Common;
using Common.Exceptions;
using FoodStuff.API.Models;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.v1.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class RecipeCategoryController : BaseController
    {
        private readonly IRecipeCategory _RecipeCategory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public RecipeCategoryController(IRecipeCategory recipeCategory, IMediator mediator, IMapper mapper)
        {
            _RecipeCategory = recipeCategory;
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(RecipeCategoreDTO isRecipeCategoreDTO, CancellationToken cancellationToken)
        {
            try
            {
                RecipeCategore RecipeCategore = new RecipeCategore();

                var name = await _mediator.Send(new CreateTranslationCommand
                {
                    Translation = isRecipeCategoreDTO.Name.ToEntity(_mapper)
                }, cancellationToken);
                RecipeCategore.NameId = name.Id;
                await _RecipeCategory.AddAsync(RecipeCategore, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }

        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put(RecipeCategoreDTO isRecipeCategoreDTO, CancellationToken cancellationToken)
        {
            try
            {
                RecipeCategore RecipeCategore = new RecipeCategore();

                var name = await _mediator.Send(new UpdateTranslationCommand
                {
                    Translation = isRecipeCategoreDTO.Name.ToEntity(_mapper)
                }, cancellationToken);
                RecipeCategore.NameId = name.Id;
                RecipeCategore.Id = isRecipeCategoreDTO.Id;
                await _RecipeCategory.UpdateAsync(RecipeCategore, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            RecipeCategore RecipeCategore = await _RecipeCategory.Table.Where(x => x.Id == id).FirstAsync();
            await _RecipeCategory.DeleteAsync(RecipeCategore, cancellationToken);
            return Ok();
        }

        [HttpGet("GetAllAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<RecipeCategoreDTO>> GetAllAsync(int? page, int pageSize
        , CancellationToken cancellationToken)
        {
            var listRecipeCategory = await _RecipeCategory.Table
                .Include(x => x.NameTranslation)
                .ToListAsync();

            var countDetails = listRecipeCategory.Count();
            List<RecipeCategoreDTO> paging = new List<RecipeCategoreDTO>();

            if (listRecipeCategory.Count > 0)
            {
                foreach (var item in listRecipeCategory)
                {
                    RecipeCategoreDTO invoicePaging = new RecipeCategoreDTO()
                    {
                        Id = item.Id,
                        Name = new TranslationDto()
                        {
                            Id = item.NameId,
                            Arabic = item.NameTranslation.Arabic,
                            English = item.NameTranslation.English,
                            Persian = item.NameTranslation.Persian
                        }
                    };

                    paging.Add(invoicePaging);
                }
            }

            var result = new PageResult<RecipeCategoreDTO>
            {
                Count = countDetails,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = paging
            };

            return result;
        }
    }
}
