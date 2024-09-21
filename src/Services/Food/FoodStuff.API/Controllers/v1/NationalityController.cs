using AutoMapper;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.Food;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    [Authorize(Roles = "Admin")]
    public class NationalityController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Nationality> _repository;
        private readonly ITranslationRepository _translationRepository;
        private readonly IRepository<FoodNationality> _foodNationalityRepository;
        private readonly IRepository<DietPackNationality> _dietPackNationalityRepository;

        public NationalityController(IMapper mapper, ITranslationRepository translationRepository,
            IRepository<Nationality> repository, IRepository<FoodNationality> foodNationalityRepository,
            IRepository<DietPackNationality> dietPackNationalityRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _translationRepository = translationRepository;
            _foodNationalityRepository = foodNationalityRepository;
            _dietPackNationalityRepository = dietPackNationalityRepository;
        }


        [HttpPost]
        public async Task<ApiResult> PostAsync(NationalityDTO nationalityDto, CancellationToken cancellationToken)
        {
            bool isExist = await _repository.TableNoTracking.Include(t => t.NameTranslation)
                .AnyAsync(n => n.NameTranslation.Persian == nationalityDto.NameTranslation.Persian, cancellationToken);

            if (isExist)
                throw new AppException(ApiResultStatusCode.BadRequest, "نام ملیت تکراری است");

            var nationality = nationalityDto.ToEntity(_mapper);

            var translation = nationalityDto.NameTranslation.ToEntity(_mapper);

            translation = await _translationRepository.AddAsync(translation, cancellationToken);
            nationality.NameId = translation.Id;
            await _repository.AddAsync(nationality, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<ApiResult> PutAsync(NationalityDTO nationalityDto, CancellationToken cancellationToken)
        {
            var nationality = nationalityDto.ToEntity(_mapper);

            var translation = nationalityDto.NameTranslation.ToEntity(_mapper);

            await _translationRepository.UpdateAsync(translation, cancellationToken);

            await _repository.UpdateAsync(nationality, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ApiResult<List<Nationality>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking
                .Include(t => t.NameTranslation).ToListAsync(cancellationToken);
            return new ApiResult<List<Nationality>>(
                true,
                ApiResultStatusCode.Success,
                result);
        }

        [HttpDelete]
        public async Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var nationality = await _repository.Table.Include(fn => fn.FoodNationalities)
                .Include(dpn => dpn.DietPackNationalities).FirstOrDefaultAsync(n => n.Id == id);


            await _foodNationalityRepository.DeleteRangeAsync(nationality.FoodNationalities, cancellationToken);

            await _dietPackNationalityRepository.DeleteRangeAsync(nationality.DietPackNationalities, cancellationToken);

            await _repository.DeleteAsync(nationality, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }
    }
}