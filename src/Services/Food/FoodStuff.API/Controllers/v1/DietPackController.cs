using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using Dapper;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.API.Models.DTOs;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.v1.Command.DietPackCommand;
using FoodStuff.Service.v1.Query.DietPackQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.v1.Command;
using Service.v1.Query;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.Models;
using WebFramework.Api;


namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class DietPackController : BaseController
    {
        private readonly IRepository<DietPack> _Repository;
        private readonly IFoodRepository _foodRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRepository<DietPackAlerge> _alergyRepository;

        private readonly IRepository<DietCategory> _categoryrepository;
        private readonly IRepository<DietPackFood> _dietpackfoodrepository;
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IRepositoryRedis<DietPack> _repositoryRedis;
        private readonly IRepository<MeasureUnit> _measureUnitRepository;
        private readonly IRepository<DietPackNationality> _dietpackNationalityRepository;
        private readonly IRepository<DietPackDietCategory> _dietPackDietCategoryRepository;
        private readonly IRepository<DietPackSpecialDisease> _dietPackSpecialDiseaseRepository;
        private readonly IRepository<UserTrackDietPackDetail> _UserTrackDietPackDetail;
        private readonly IRepository<Food> _Food;
        private readonly IRepository<Translation> _Translation;
        private readonly IRepository<MeasureUnit> _MeasureUnit;
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IRepository<DietPack> _dietpackRepository;
        private readonly IRepository<DietPackAlerge> _dietPackAllerge;
        public DietPackController(IFoodRepository foodRepository, IMediator mediator, IMapper mapper,
            IRepository<DietPack> Repository, IRepository<DietPackAlerge> repositoryAs,
            IRepository<DietPackFood> dietpackfoodrepository,
            IRepository<DietPackAlerge> alergyRepository,
            IRepository<DietPackNationality> dietpackNationalityRepository,
            IRepository<DietCategory> categoryrepository,
            IRepository<DietPackDietCategory> dietPackDietCategoryRepository,
            IRepositoryRedis<DietPack> repositoryRedis,
            IRepository<MeasureUnit> measureUnitRepository,
            IRepository<DietPackSpecialDisease> dietPackSpecialDiseaseRepository
            , IDatabaseConnectionFactory connectionFactory
            , IRedisCacheClient redisCacheClient, IRepository<Food> food, IRepository<MeasureUnit> measureUnit, IRepository<Translation> translation, IRepository<DietPack> dietpackRepository, IRepository<DietPackAlerge> dietPackAllerge)
        {
            _mediator = mediator;
            _mapper = mapper;
            _foodRepository = foodRepository;
            _Repository = Repository;
            _dietpackfoodrepository = dietpackfoodrepository;
            _alergyRepository = alergyRepository;
            _categoryrepository = categoryrepository;
            _dietPackDietCategoryRepository = dietPackDietCategoryRepository;
            _repositoryRedis = repositoryRedis;
            _measureUnitRepository = measureUnitRepository;
            _dietpackNationalityRepository = dietpackNationalityRepository;
            _dietPackSpecialDiseaseRepository = dietPackSpecialDiseaseRepository;
            _connectionFactory = connectionFactory;
            _redisCacheClient = redisCacheClient;
            _Food = food;
            _MeasureUnit = measureUnit;
            _Translation = translation;
            _dietpackRepository = dietpackRepository;
            _dietPackAllerge = dietPackAllerge;
        }
        

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAsync(CreateDietPackDTO createDietPackDto, CancellationToken cancellationToken)
        {

            string persian = "";
            string arabic = "";
            string english = "";
            foreach (var item in createDietPackDto.DietPackFoods)
            {
                var currentfood = await _foodRepository.Table
                    .Where(f => f.Id == item.FoodId)
                    .Include(t => t.TranslationName)
                    .Select(s => new TranslationDto
                    {
                        Persian = s.TranslationName.Persian,
                        Arabic = s.TranslationName.Arabic,
                        English = s.TranslationName.English,
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                persian += currentfood.Persian + " - ";
                arabic += currentfood.Arabic + " - ";
                english += currentfood.English + " - ";
            }

            Translation Name = new Translation
            {
                Persian = persian,
                Arabic = arabic,
                English = english
            };

            foreach (var dietCategoryId in createDietPackDto.DietCategoryIds)
            {
                var isExist = await _Repository.TableNoTracking
               .Include(s => s.Name)
               .Include(dc => dc.DietPackDietCategories)
               .AnyAsync(d => d.CaloriValue == createDietPackDto.CalorieValue &&
               d.Name.Persian == Name.Persian &&
               d.DailyCalorie == createDietPackDto.DailyCalorie &&
               d.FoodMeal == createDietPackDto.FoodMeal &&
               d.DietPackDietCategories.Any(dc => dc.DietCategoryId == dietCategoryId));
                if (isExist)
                {
                    throw new AppException("DietPack Is Duplicate");
                }
            }


            var _name = await _mediator.Send(new CreateTranslationCommand
            {
                Translation = Name
            });


            await _mediator.Send(new CreateDietPackCommand
            {
                CalorieValue = createDietPackDto.CalorieValue,
                DietCategoryIds = createDietPackDto.DietCategoryIds,
                IsActive = createDietPackDto.IsActive,
                DietPackFoods = createDietPackDto.DietPackFoods,
                NameId = _name.Id,
                FoodMeal = createDietPackDto.FoodMeal,
                NutrientValue = createDietPackDto.NutrientValue,
                NationalityIds = createDietPackDto.NationalityIds,
                CategoryId = createDietPackDto.CategoryId,
                DailyCalorie = createDietPackDto.DailyCalorie
            }, cancellationToken);

            return Ok();
        }


        [Route("GetAll")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(string name, double DailyCalorie, int? Meal,
         int DietCategoryId, int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var dietpaks = await _mediator.Send(new GetAllDietPackQuery
            {
                Name = name,
                DailyCalorie = DailyCalorie,
                Meal = Meal,
                Page = Page,
                PageSize = PageSize,
                Language = LanguageName == null ? "Persian" : LanguageName,
                DietCategoryId = DietCategoryId
            }, cancellationToken);

            return Ok(dietpaks);

        }



        [HttpGet("GetById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var dietpaks = await _mediator.Send(new GetDietPackByIdQuery
            {
                Id = id
            }, cancellationToken);

            return Ok(dietpaks);
        }

        // [HttpGet]
        // [Authorize(Roles = "Admin")]
        // public async Task<IActionResult> GetAll()
        // {
        //
        // }

        // [HttpGet("GetByIdList")]
        // public async Task<List<GetDietPackViewModel>> GetByIdList(int id, CancellationToken cancellationToken)
        // {
        //
        //     var _list = await _mediator.Send(new GetByIdsListQuery
        //     {
        //         Id = id
        //     });
        //     return _list;
        // }
        //

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAsync(UpdateDietPackDTO updateDietPackDto, CancellationToken cancellationToken)
        {
            var currentPack = await _Repository.Table.Where(a => a.Id == updateDietPackDto.Id)
               .FirstOrDefaultAsync(cancellationToken);



            string persian = "";
            string arabic = "";
            string english = "";
            foreach (var item in updateDietPackDto.DietPackFoods)
            {
                var Currentfood = await _foodRepository.Table.Where(f => f.Id == item.FoodId)
                    .Include(f => f.TranslationName)
                    .FirstOrDefaultAsync(cancellationToken);


                persian += Currentfood.TranslationName.Persian + " - ";
                arabic += Currentfood.TranslationName.Arabic + " - ";
                english += Currentfood.TranslationName.English + " - ";
            }

            Translation name = new Translation
            {
                Persian = persian,
                Arabic = arabic,
                English = english
            };


            var translationName = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = name
            });


            var dietpack = await _mediator.Send(new EditDietPackCommand
            {
                Id = updateDietPackDto.Id,
                CalorieValue = updateDietPackDto.CalorieValue,
                DietPackFoods = updateDietPackDto.DietPackFoods,
                NameId = translationName.Id,
                FoodMeal = updateDietPackDto.FoodMeal,
                IsActive = updateDietPackDto.IsActive,
                NutrientValue = updateDietPackDto.NutrientValue,
                NationalityIds = updateDietPackDto.NationalityIds,
                DietCategoryIds = updateDietPackDto.DietCategoryIds,
                DailyCalorie = updateDietPackDto.DailyCalorie
            });

            return Ok();
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync([Required] int id, CancellationToken cancellationToken)
        {

            DietPack dietPack = await _Repository
              .Table.Include(a => a.DietPackAlerges)
              .Include(c => c.DietPackDietCategories)
              .Include(d => d.DietPackNationalities)
              .Include(a => a.DietPackFoods)
              .Include(s => s.DietPackSpecialDiseases)
              .Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);

            List<int> _list = new List<int>
            {
                dietPack.NameId
            };
            await _dietPackSpecialDiseaseRepository.DeleteRangeAsync(dietPack.DietPackSpecialDiseases, cancellationToken);
            await _alergyRepository.DeleteRangeAsync(dietPack.DietPackAlerges, cancellationToken);
            await _dietpackNationalityRepository.DeleteRangeAsync(dietPack.DietPackNationalities, cancellationToken);
            await _dietPackDietCategoryRepository.DeleteRangeAsync(dietPack.DietPackDietCategories, cancellationToken);
            await _dietpackfoodrepository.DeleteRangeAsync(dietPack.DietPackFoods, cancellationToken);

            await _Repository.DeleteAsync(dietPack, cancellationToken);

            await _mediator.Send(new DeleteTranslationCommand
            {
                Ids = _list
            });


            return Ok();
        }


        [HttpGet("Search")]
        [Authorize]
        public async Task<PageResult<DietPackSearchResultViewModel>> Search(
            [FromQuery] SearchDietPackDTO searchDietPackDto, CancellationToken cancellationToken)
        {
            PageResult<DietPackSearchResultViewModel> result = new PageResult<DietPackSearchResultViewModel>();


            result = await _mediator.Send(new SearchDietPackQuery
            {
                Name = searchDietPackDto.Name,
                Page = searchDietPackDto.Page,
                PageSize = searchDietPackDto.PageSize,
                Language = LanguageName == null ? "Persian" : LanguageName,
                FoodMeal = searchDietPackDto.FoodMeal,
                DietCategoryIds = searchDietPackDto.DietCategoryIds,
                SpecialDiseases = searchDietPackDto.SpecialDiseases,
                CalorieValue = searchDietPackDto.CalorieValue,
                IngredientAllergyIds = searchDietPackDto.Allergies,
                NationalityIds = searchDietPackDto.NationalityIds,
                DailyCalorie = searchDietPackDto.DailyCalorie
            }, cancellationToken);





            return result;
        }

        [HttpGet("GetAllFullDietPack")]
        public async Task<PageResult<GetAllFullDietPackViewModel>> GetAllFullDietPack(int? Page, int PageSize)
        {
            var result = await _mediator.Send(new GetAllFullDietPackQuery
            {
                Page = Page,
                PageSize = PageSize
            });
            return result;
        }

        /// <summary>
        ///  پکیج های فعال
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        // [HttpGet("GetAllActiveDietPackagesAsync")]
        // //[Authorize(Roles = "Admin")]
        // [AllowAnonymous]
        // public async Task<PageResult<GetAllQueryResult>> GetAllActiveDietPackagesAsync(int? Page, int PageSize, CancellationToken cancellationToken)
        // {
        //     var dietpaks = await _mediator.Send(new GetAllQuery
        //     {
        //         Page = Page,
        //         PageSize = PageSize
        //     });
        //
        //     return dietpaks;
        // }
        //

        //
        // [HttpGet("GetDietPacksForNutritionist")]
        // [AllowAnonymous]
        // public async Task GetDietPacksForNutritionist(GetDietPacksForNutritionistDto dietPacksForNutritionistDto)
        // {
        //
        // }

        [HttpGet("GetUserPackage")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ApiResult<List<DietPackDto>>> GetUserPackage([FromQuery] GetDietPackFoodViewModel dto,
            CancellationToken cancellationToken)
        {

            double calorieResult = 0;

            switch (string.Compare(dto.DailyCalorie.ToString().Substring(2, 2), "50"))
            {
                case 0:
                    calorieResult = dto.DailyCalorie;
                    break;
                case -1:
                    calorieResult = dto.DailyCalorie.ToString().Replace(dto.DailyCalorie.ToString().Substring(2, 2), "00").ToDouble();
                    break;
                case 1:
                    calorieResult = dto.DailyCalorie.ToString().Replace(dto.DailyCalorie.ToString().Substring(2, 2), "50").ToDouble();
                    break;
                default:
                    break;
            }
            //با آلرژی
            if (!string.IsNullOrEmpty(dto.AllergyIds))
            {
                var allergyIds = dto.AllergyIds.Split(',');

                string allergyKey = string.Empty;
                foreach (var id in allergyIds)
                {
                    allergyKey += "_" + Convert.ToString(id);
                }

                int[] allergyIdsInt = new int[allergyIds.Count()];
                for (var index = 0; index < allergyIds.Length; index++)
                {
                    var id = allergyIds[index];
                    var intId = Convert.ToInt32(id);
                    allergyIdsInt[index] = intId;
                }

                if (await _redisCacheClient.Db12.ExistsAsync($"DietPackFoods_{calorieResult}_{dto.DietCategoryId}{allergyKey}"))
                {
                    return await _redisCacheClient.Db12.GetAsync<List<DietPackDto>>
                        ($"DietPackFoods_{calorieResult}_{dto.DietCategoryId}{allergyKey}");
                }

                var dietPacks = await _dietpackRepository.Table.AsNoTracking().Where(x => x.DailyCalorie == calorieResult && x.IsActive)
                    .Include(x => x.DietPackDietCategories).ThenInclude(x => x.DietCategory)
                    .Include(x => x.Name).Include(x => x.DietPackFoods).ThenInclude(x => x.Food).ThenInclude(x => x.TranslationName)
                    .Include(x => x.DietPackFoods).ThenInclude(x => x.MeasureUnit).ThenInclude(x => x.Translation)
                    .Where(x => x.DietPackDietCategories.Any(q => q.DietCategory.IsActive && q.DietCategoryId == dto.DietCategoryId))
                    .ToListAsync(cancellationToken).ConfigureAwait(false);

                var allergyIdsFromDb = await _alergyRepository.TableNoTracking
                    .Where(x => allergyIdsInt.Contains(x.IngredientAllergyCategoryId)).Select(s => s.DietPackId)
                    .ToListAsync(cancellationToken).ConfigureAwait(false);
                var packs = dietPacks.Where(x => !allergyIdsFromDb.Contains(x.Id)).ToList();
                //var dietPacks = await _dietpackRepository.Table.AsNoTracking().Where(x => x.DailyCalorie == calorieResult && x.IsActive )
                //    .Include(x=>x.DietPackDietCategories).ThenInclude(x=>x.DietCategory)
                //    .Include(x => x.DietPackAlerges)
                //    .Include(x => x.Name).Include(x => x.DietPackFoods).ThenInclude(x => x.Food).ThenInclude(x => x.TranslationName)
                //    .Include(x => x.DietPackFoods).ThenInclude(x => x.MeasureUnit).ThenInclude(x => x.Translation)
                //    .Where(x => x.DietPackAlerges.Any(q => !allergyIdsInt.Contains(q.IngredientId)))
                //    .Where(x=>x.DietPackDietCategories.Any(q=>q.DietCategory.IsActive && q.DietCategoryId == dto.DietCategoryId))
                //    .ToListAsync(cancellationToken).ConfigureAwait(false);

                var result = _mapper.Map<List<DietPack>, List<DietPackDto>>(packs);

                if (result.Count == 0)
                {
                    return new ApiResult<List<DietPackDto>>(false, ApiResultStatusCode.BadRequest,
                        new List<DietPackDto>());
                }

                await _redisCacheClient.Db12
                    .AddAsync($"DietPackFoods_{calorieResult}_{dto.DietCategoryId}{allergyKey}", result,expiresIn: TimeSpan.FromDays(90));
                return new ApiResult<List<DietPackDto>>(true, ApiResultStatusCode.Success, result);

            }
            //بدون آلرژی
            else
            {
                if (await _redisCacheClient.Db12.ExistsAsync($"DietPackFoods_{calorieResult}_{dto.DietCategoryId}"))
                {
                    return await _redisCacheClient.Db12.GetAsync<List<DietPackDto>>
                        ($"DietPackFoods_{calorieResult}_{dto.DietCategoryId}");
                }

                var dietPacks = await _dietpackRepository.Table.AsNoTracking().Where(x => x.DailyCalorie == calorieResult && x.IsActive )
                    .Include(x => x.DietPackDietCategories).ThenInclude(x => x.DietCategory)
                    .Include(x => x.Name).Include(x => x.DietPackFoods).ThenInclude(x => x.Food).ThenInclude(x => x.TranslationName)
                    .Include(x => x.DietPackFoods).ThenInclude(x => x.MeasureUnit).ThenInclude(x => x.Translation)
                    .Where(x => x.DietPackDietCategories.Any(q => q.DietCategory.IsActive && q.DietCategoryId == dto.DietCategoryId))
                    .ToListAsync(cancellationToken).ConfigureAwait(false);

                var result = _mapper.Map<List<DietPack>, List<DietPackDto>>(dietPacks);
                
                if (result.Count == 0)
                {
                    return new ApiResult<List<DietPackDto>>(false, ApiResultStatusCode.BadRequest,
                        new List<DietPackDto>());
                }

                await _redisCacheClient.Db12
                    .AddAsync($"DietPackFoods_{calorieResult}_{dto.DietCategoryId}", result, expiresIn: TimeSpan.FromDays(90));
                return new ApiResult<List<DietPackDto>>(true, ApiResultStatusCode.Success, result);

            }


            //daily calories  رند میشه به پایین

            //get data from redis if exists

            //table DietPackDietCategories return DietPackId
            //var DietPackIds = await _categoryrepository.Table
            //.Include(x => x.DietPackDietCategories)
            //.Where(x => x.Id == model.DietCategoryId)
            //.Select(x => x.DietPackDietCategories.Select(y => y.DietPackId)).FirstOrDefaultAsync();

            // using var conn = await _connectionFactory.CreateConnectionAsync();
            //first select from DietPack where DailyCalorie == switch Daily calories up
            //second select get all allergies from DietPackAlerges
            // string queryDietPack = $"select * from public.\"DietPacks\"\r\nwhere(\"DietPacks\".\"IsActive\"=true AND \"DietPacks\".\"DailyCalorie\" ={calorieResult});" +
            //     $"select * from public.\"DietPackAlerges\";";

            // using (var multi = conn.QueryMultiple(queryDietPack))
            // {
            //     List<DietPack> dietPacks = multi.ReadAsync<DietPack>().Result.Where(x => DietPackIds.Contains(x.Id)).ToList();
            //     List<DietPackAlerge> dietPackAlerges = multi.ReadAsync<DietPackAlerge>().Result.ToList();
            //     List<DietPackFood> dietPackFoods = _dietpackfoodrepository.Table.Where(x => dietPacks.Select(x => x.Id).Contains(x.DietPackId)).ToList();
            //     var Translations = _Translation.Table.AsQueryable();
            //     var MeasureUnits = _MeasureUnit.Table.Include(x => x.Translation).AsQueryable();
            //     var Foods = _Food.Table.Include(x => x.TranslationName).AsQueryable();


            //foreach (var allergy in allergyIds)
            //{
            //    var allergyId = Convert.ToInt32(allergy);
            //    var test = dietPacks.Where(x => x.DietPackAlerges.Where(w => w.Id == allergyId)).ToList();
            //} 
            //foreach (var dietPack in dietPacks)
            //{
            //    dietPack.DietPackAlerges =
            //        _dietPackAllerge.TableNoTracking.Where(x => x.DietPackId == dietPack.Id).ToList().Count > 0
            //            ? _dietPackAllerge.TableNoTracking.Where(x => x.DietPackId == dietPack.Id).ToList() : new List<DietPackAlerge>();
            //    // dietPack.Name = _Translation.TableNoTracking.FirstOrDefault(x => x.Id == dietPack.Id);
            //}


            //foreach (var dietPack in dietPacks)
            //{
            //    DietPacksList.Add(new DietPack
            //    {
            //        Id = dietPack.Id,
            //        DietPackFoods = dietPackFoods.Where(x => x.DietPackId == dietPack.Id).Select(x => new DietPackFood
            //        {
            //            Calorie = x.Calorie,
            //            Food = Foods.Where(f => f.Id == x.FoodId).FirstOrDefault(),
            //            FoodId = x.FoodId,
            //            Id = x.Id,
            //            CategoryChildId = x.CategoryChildId,
            //            DietPackId = dietPack.Id,
            //            MeasureUnitId = x.MeasureUnitId,
            //            MeasureUnit = MeasureUnits.Where(y => y.Id == x.MeasureUnitId).FirstOrDefault(),
            //            NutrientValue = x.NutrientValue,
            //            Value = x.Value,
            //
            //        }).ToList(),
            //        CaloriValue = dietPack.CaloriValue,
            //        CategoryId = dietPack.CategoryId,
            //        DailyCalorie = dietPack.DailyCalorie,
            //        DietPackAlerges = dietPackAlerges.Where(x => x.DietPackId == dietPack.Id).ToList().Count > 0 ? dietPackAlerges.Where(x => x.DietPackId == dietPack.Id).ToList() : new List<DietPackAlerge>(),
            //        FoodMeal = dietPack.FoodMeal,
            //        IsActive = dietPack.IsActive,
            //        Name = Translations.Where(t => t.Id == dietPack.NameId).FirstOrDefault(),
            //        NutrientValue = dietPack.NutrientValue,
            //        NameId = dietPack.NameId
            //
            //    });
            //
            //}

            //}

            // List<GetDietPackViewModel> result = new List<GetDietPackViewModel>();
            // foreach (var i in DietPacksList)
            // {
            //     if (!i.DietPackAlerges.Any(x => model.AlergyIds.Contains(x.Id)))
            //     {
            //         result.Add(new GetDietPackViewModel
            //         {
            //             CalorieValue = i.CaloriValue,
            //             CategoryId = i.CategoryId,
            //             DailyCalorie = i.DailyCalorie,
            //             //DietCategoryIds = i.DietPackDietCategories.Select(x => x.Id).ToList(),
            //             Id = i.Id,
            //             DietPackFoods = i.DietPackFoods.Select(sf => new DietPackFoodViewModel
            //             {
            //                 Calorie = sf.Calorie,
            //                 CategoryChildId = sf.CategoryChildId,
            //                 FoodId = sf.FoodId,
            //                 MeasureUnitId = sf.MeasureUnitId,
            //                 MeasureUnitName = sf.MeasureUnit.Translation.Persian,
            //                 FoodName = sf.Food.TranslationName.Persian,
            //                 MeasureUnitValue = sf.Value,
            //                 //NutrientValue = sf.NutrientValue != null ? StringConvertor.ToNumber(sf.NutrientValue) : new List<double>()
            //             }).ToList(),
            //             Name = i.Name.Persian
            //         });
            //     }
            //
            // }


        }
        #region Diet Edit Force Name Update


        [HttpPut("DietForceNameUpdate")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> DietForceNameUpdate(int minId, int maxId, string password, CancellationToken cancellationToken)
        {
            try
            {
                if (password.Contains("ali8345"))
                {
                    var packages = _Repository.Table.AsNoTracking().Where(c => c.Id >= minId && c.Id <= maxId).ToList();
                    var pkList = new List<DietPack>();
                    foreach (var pk in packages)
                    {
                        var currentpack = pk;//await _Repository.Table.Where(a => a.Id == dietPackDTO.Id)
                                             //.FirstOrDefaultAsync(cancellationToken);

                        List<int> arrayfoodid = new List<int>();



                        List<int> nameIds = new List<int>();

                        var pkFoods = _dietpackfoodrepository.TableNoTracking.Where(d => d.DietPack == pk).ToList();
                        string FoodtagFa = "";
                        string FoodtagArEn = "";
                        foreach (var item in pkFoods)
                        {
                            var Currentfood = _foodRepository.GetByIdAsync(cancellationToken, item.FoodId).Result;

                            nameIds.Add(Currentfood.NameId);
                            FoodtagFa += Currentfood.Tag + " - ";
                            FoodtagArEn += Currentfood.TagArEn + " - ";
                            _dietpackfoodrepository.Detach(item);
                            _foodRepository.Detach(Currentfood);

                        }

                        TranslationDto Name = new TranslationDto();
                        var Persiantranslations = await _mediator.Send(new GetTranslationQuery()
                        {
                            Ids = nameIds,
                            Language = "Persian"
                        });

                        var Englishtranslations = await _mediator.Send(new GetTranslationQuery()
                        {
                            Ids = nameIds,
                            Language = "English"
                        });

                        var Arabictranslations = await _mediator.Send(new GetTranslationQuery()
                        {
                            Ids = nameIds,
                            Language = "Arabic"
                        });


                        foreach (var item in Persiantranslations)
                        {
                            Name.Persian += item.Text + " - ";
                        }
                        Name.Persian = Name.Persian;
                        foreach (var item in Englishtranslations)
                        {
                            Name.English += item.Text + " - ";
                        }
                        Name.English = Name.English;

                        foreach (var item in Arabictranslations)
                        {
                            Name.Arabic += item.Text + " - ";
                        }
                        Name.Arabic = Name.Arabic;


                        var _name = await _mediator.Send(new CreateTranslationCommand
                        {
                            Translation = Name.ToEntity(_mapper)
                        });

                        pk.NameId = _name.Id;
                        pkList.Add(pk);

                        //else
                        //{
                        //    Name.Id = currentpack.NameId;

                        //    var _name = await _mediator.Send(new UpdateTranslationCommand
                        //    {
                        //        Translation = Name.ToEntity(_mapper)
                        //    });
                        //}

                    }
                    _Repository.UpdateRange(pkList);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        // ویرایش مواد مغذی های غذا برای پک های رژیم که شامل یک غذای خاص باشد
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> EditPackageContainAFood(int foodId, CancellationToken cancellationToken)
        {
            try
            {
                var pkList = new List<DietPack>();
                var dietPakFoods = _dietpackfoodrepository.Table.Include(p => p.DietPack).AsNoTracking().Where(d => d.FoodId == foodId);
                var pkIds = dietPakFoods.Select(d => d.DietPack.Id).Distinct().ToList();
                foreach (var id in pkIds)
                {
                    var pk = _Repository.TableNoTracking.Include(d => d.DietPackFoods).FirstOrDefault(d => d.Id == id);
                    var foods = new List<IngMeasurModel>();
                    foreach (var dietPkFood in pk.DietPackFoods)
                    {
                        foods.Add(new IngMeasurModel()
                        {
                            Id = dietPkFood.FoodId,
                            MeasureUnitId = dietPkFood.MeasureUnitId,
                            Value = dietPkFood.Value
                        });
                    }
                    List<double> Nutrients = new List<double>();
                    foreach (var item in foods)
                    {
                        var food = _foodRepository.TableNoTracking.FirstOrDefault(f => f.Id == item.Id);
                        var foodNutrients = StringConvertor.ToNumber(food.NutrientValue);
                        var measurValue = _measureUnitRepository.TableNoTracking.FirstOrDefault(m => m.Id == item.MeasureUnitId);
                        if (Nutrients.Count() > 0)
                        {
                            for (int i = 0; i < foodNutrients.Count(); i++)
                            {
                                Nutrients[i] = Nutrients[i] + ((foodNutrients[i] * measurValue.Value * item.Value) / 100);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foodNutrients.Count(); i++)
                            {
                                foodNutrients[i] = (foodNutrients[i] * measurValue.Value * item.Value) / 100;
                            }
                            Nutrients = foodNutrients;
                        }
                    }
                    int h = 1;
                    string str = "";
                    foreach (var item in Nutrients)
                    {
                        if (1 < h && h < 35)
                        {
                            str = str + "," + item;
                        }
                        if (h == 1)
                        {
                            str = item.ToString();
                        }
                        h++;
                    }
                    pk.NutrientValue = str;
                    pkList.Add(pk);
                }
                await _Repository.UpdateRangeAsync(pkList, cancellationToken);
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + "خطا در ویرایش");
            }
        }

        #endregion

        
    }

}


