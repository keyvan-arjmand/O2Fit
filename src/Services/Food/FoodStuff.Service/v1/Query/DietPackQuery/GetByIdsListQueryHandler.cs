// using Common;
// using Data.Contracts;
// using FoodStuff.Domain.Entities.Diet;
// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using Newtonsoft.Json;
// using Service.v1.Query;
// using StackExchange.Redis;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using FoodStuff.Domain.Entities.Translation;
// using FoodStuff.Domain.Entities.ViewModels;
//
// namespace FoodStuff.Service.v1.Query.DietPackQuery
// {
//     public class GetByIdsListQueryHandler : IRequestHandler<GetByIdsListQuery, List<GetDietPackViewModel>>,
//         ITransientDependency
//     {
//         private readonly IRepository<DietPack> _repository;
//         private readonly IMediator _mediator;
//         private readonly IRepositoryRedis<DietPack> _repositoryRedis;
//
//         public GetByIdsListQueryHandler(IRepository<DietPack> repository, IRepositoryRedis<DietPack> repositoryRedis
//             , IMediator mediator)
//         {
//             _repository = repository;
//             _repositoryRedis = repositoryRedis;
//             _mediator = mediator;
//         }
//
//         public async Task<List<GetDietPackViewModel>> Handle(GetByIdsListQuery request,
//             CancellationToken cancellationToken)
//         {
//             List<DietPack> _GetdietPacks = new List<DietPack>();
//             List<string> listdietid = new List<string>();
//             KeyValuePair<RedisKey, RedisValue>[] keyValuePairs = null;
//             var result = new List<GetDietPackViewModel>();
//
//             foreach (var item in request.Ids)
//             {
//                 listdietid.Add($"Diet_{item}");
//             }
//
//             _GetdietPacks = await _repositoryRedis.GetAllAsync(listdietid);
//
//
//             if (_GetdietPacks.Count == 0)
//             {
//                 _GetdietPacks = await _repository.TableNoTracking.Where(a => request.Ids.Contains(a.Id))
//                     .ToListAsync(cancellationToken);
//
//                 keyValuePairs = new KeyValuePair<RedisKey, RedisValue>[_GetdietPacks.Count];
//
//                 for (int i = 0; i < _GetdietPacks.Count; i++)
//                 {
//                     string _json = JsonConvert.SerializeObject(_GetdietPacks[i]);
//                     keyValuePairs[i] = new KeyValuePair<RedisKey, RedisValue>($"Diet_{_GetdietPacks[i].Id}", _json);
//                 }
//
//                 await _repositoryRedis.UpdateAllAsync(keyValuePairs);
//
//             }
//             else
//             {
//                 int checkCount = _GetdietPacks.Where(a => request.Ids.Contains(a.Id)).Count();
//
//                 if (checkCount != listdietid.Count)
//                 {
//                     _GetdietPacks = await _repository.TableNoTracking.Where(a => request.Ids.Contains(a.Id))
//                         .ToListAsync();
//
//                     keyValuePairs = new KeyValuePair<RedisKey, RedisValue>[_GetdietPacks.Count];
//
//                     for (int i = 0; i < _GetdietPacks.Count; i++)
//                     {
//                         string _json = JsonConvert.SerializeObject(_GetdietPacks[i]);
//                         keyValuePairs[i] =
//                             new KeyValuePair<RedisKey, RedisValue>($"Translation_Food_{_GetdietPacks[i].Id}", _json);
//                     }
//
//                     await _repositoryRedis.UpdateAllAsync(keyValuePairs);
//
//                 }
//             }
//
//             if (_GetdietPacks.Count > 0)
//             {
//                 foreach (var item in _GetdietPacks)
//                 {
//                     GetDietPackViewModel getDietPackViewModel = new GetDietPackViewModel
//                     {
//                         Id = item.Id,
//                         CaloriValue = item.CaloriValue,
//                         //DietCategory = item.DietCategory.Id,
//                         DietPackAlerges = item.DietPackAlerges.Select(a => a.IngredientId).ToList(),
//                         DietPackFoods = item.DietPackFoods.Select(a => new DietPackFoodDtoResult
//                         {
//                             FoodId = a.FoodId,
//                             FoodTranslation = new Translation
//
//                                 Arabic = a.Food.TranslationName.Arabic,
//                             Persian = a.Food.TranslationName.Persian,
//                             English = a.Food.TranslationName.English,
//                             Id = a.Food.TranslationName.Id,
//
//                         },
//                             MeasureUnit = new Domain.Entities.MeasureUnit.MeasureUnit
//                             {
//                                 Id = a.MeasureUnitId,
//                                 Translation = new Domain.Entities.Translation.Translation
//                                 {
//                                     Arabic = a.MeasureUnit.Translation.Arabic,
//                                     Persian = a.MeasureUnit.Translation.Persian,
//                                     English = a.MeasureUnit.Translation.English,
//                                     Id = a.Food.TranslationName.Id,
//                                 }
//                             },
//                             MeasureUnitId = a.MeasureUnitId,
//                             Id = a.Id,
//                             Value = a.Value
//                     }).ToList(),
//                     FoodMeal = (int)item.FoodMeal,
//
//                     NutrientValue = item.NutrientValue
//
//                 }
//
//                 
//
//                 result.Add(getDietPackViewModel);
//             }
//
//
//             return result;
//         }
//     }
// }
//
