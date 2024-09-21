using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.Domain.Entities.Food;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;
        private readonly ITranslationRepository _translationRepository;
        private readonly IRepository<FoodCategory> _foodCategoryRepository;

        public CategoryController(IMapper mapper, ITranslationRepository translationRepository,
            IRepository<Category> repository, IRepository<FoodCategory> foodCategoryRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _translationRepository = translationRepository;
            _foodCategoryRepository = foodCategoryRepository;
        }


        [HttpPost]
        public async Task<ApiResult> PostAsync(CategoryDTO categoryDto, CancellationToken cancellationToken)
        {
            var category = categoryDto.ToEntity(_mapper);

            var translation = categoryDto.NameTranslation.ToEntity(_mapper);

            translation = await _translationRepository.AddAsync(translation, cancellationToken);
            category.NameId = translation.Id;
            await _repository.AddAsync(category, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<ApiResult> PutAsync(CategoryDTO categoryDto, CancellationToken cancellationToken)
        {
            var category = categoryDto.ToEntity(_mapper);

            var translation = categoryDto.NameTranslation.ToEntity(_mapper);

            await _translationRepository.UpdateAsync(translation, cancellationToken);

            await _repository.UpdateAsync(category, cancellationToken);
            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<ApiResult<Category>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking.
                Include(t => t.NameTranslation)
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync(cancellationToken);

            return new ApiResult<Category>(
                true,
                ApiResultStatusCode.Success,
                result
                , "");
        }

        [HttpGet("GetByParentId")]
        public async Task<ApiResult<List<Category>>> GetByParentId(int parentId, CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking.
                Include(t => t.NameTranslation)
                .Where(c => c.ParentId == parentId)
                .ToListAsync(cancellationToken);

            return new ApiResult<List<Category>>(
                true,
                ApiResultStatusCode.Success,
                result
                , "");
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult<List<Category>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking.Include(t => t.NameTranslation).ToListAsync(cancellationToken);

            return new ApiResult<List<Category>>(
                true,
                ApiResultStatusCode.Success,
                result
                , "");
        }

        [HttpDelete]
        public async Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _repository.Table.Include(t => t.NameTranslation)
                .Include(fc => fc.FoodCategories)
                .Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);


            await _foodCategoryRepository.DeleteRangeAsync(category.FoodCategories, cancellationToken);

            await _repository.DeleteAsync(category, cancellationToken);

            await _translationRepository.DeleteAsync(category.NameTranslation, cancellationToken);


            return new ApiResult(true, ApiResultStatusCode.Success);
        }
    }
}
