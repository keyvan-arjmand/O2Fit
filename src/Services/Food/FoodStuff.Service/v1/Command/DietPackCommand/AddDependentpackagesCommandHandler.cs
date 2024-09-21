// using Common;
// using Data.Contracts;
// using FoodStuff.Common.Utilities;
// using FoodStuff.Data.Contracts;
// using FoodStuff.Domain.Entities.Diet;
// using FoodStuff.Domain.Entities.Food;
// using FoodStuff.Domain.Enum;
// using FoodStuff.Service.Calculate;
// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
//
// namespace FoodStuff.Service.v1.Command.DietPackCommand
// {
//     public class AddDependentpackagesCommandHandler : IRequestHandler<AddDependentpackagesCommand, DietPack>, ITransientDependency
//     {
//         private readonly IDietPackRepository _dietpackrepository;
//         private readonly IRepository<DietPackAlerge> _alergyRepository;
//         private readonly IRepository<DietPackCountry> _countryrepository;
//         private readonly IRepository<DietCategory> _categoryrepository;
//         private readonly IRepository<FoodIngredient> _foodIngrepository;
//         private readonly IRepository<DietPackFood> _dietpackfoodrepository;
//         private readonly IRepository<DietPackSpecialDisease> _specialdiseasepackrepository;
//         private readonly IRepository<DietAlergy> _dietAlergyrepository;
//         private readonly IRepositoryRedis<DietPack> _repositoryRedis;
//         public AddDependentpackagesCommandHandler(IDietPackRepository dietpackrepository,
//         IRepository<DietPackAlerge> alergyRepository,
//         IRepository<DietPackCountry> countryrepository,
//         IRepository<DietCategory> categoryrepository,
//         IRepository<DietPackFood> dietpackfoodrepository,
//         IRepository<DietPackSpecialDisease> specialdiseasepackrepository,
//             IRepository<FoodIngredient> foodIngrepository,
//             IRepository<DietAlergy> dietAlergyrepository,
//             IRepositoryRedis<DietPack> repositoryRedis)
//         {
//             _alergyRepository = alergyRepository;
//             _dietpackrepository = dietpackrepository;
//             _countryrepository = countryrepository;
//             _categoryrepository = categoryrepository;
//             _dietpackfoodrepository = dietpackfoodrepository;
//             _specialdiseasepackrepository = specialdiseasepackrepository;
//             _foodIngrepository = foodIngrepository;
//             _dietAlergyrepository = dietAlergyrepository;
//             _repositoryRedis = repositoryRedis;
//         }
//         public async Task<DietPack> Handle(AddDependentpackagesCommand request, CancellationToken cancellationToken)
//         {
//             #region Update Base
//             DietPack dietPackbase = await _dietpackrepository
//                .Table.Include(a => a.DietPackAlerges)
//                .Include(c => c.DietCategory)
//                .Include(d => d.DietPackSpecialDiseases)
//                .Include(d => d.PackCountries)
//                .Include(a => a.DietPackFoods)
//                .Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
//
//
//             dietPackbase.BodyType = request.BodyType;
//             dietPackbase.CaloriValue = request.CaloriValue;
//             var cat = await _categoryrepository.Table
//                 .Where(a => a.Id == request.DietCategory).FirstOrDefaultAsync();
//             dietPackbase.DietCategory = cat;
//             dietPackbase.FoodMeal = request.FoodMeal;
//             dietPackbase.DietPackState = DietPackState.WatingForEdit;
//             dietPackbase.Tag = request.Tag;
//             dietPackbase.TagArEn = request.TagArEn;
//             dietPackbase.NutrientValue = StringConvertor.DoubleToString(request.NutrientValue);
//             await _dietpackrepository.UpdateAsync(dietPackbase, cancellationToken);
//
//             #region Alergy
//             List<DietPackAlerge> dietPackAlerges = new List<DietPackAlerge>();
//             if (request.DietPackFoods.Count > 0)
//             {
//                 if (dietPackbase.DietPackAlerges != null)
//                 {
//                     await _alergyRepository.DeleteRangeAsync(dietPackbase.DietPackAlerges, cancellationToken);
//                 }
//
//                 List<int> ingsLst = new List<int>();
//                 foreach (var item in request.DietPackFoods)
//                 {
//                     var ingList = await _foodIngrepository.TableNoTracking
//                         .Where(a => a.FoodId == item.FoodId).Select(a => a.IngredientId).ToListAsync();
//                     ingsLst.AddRange(ingList.Distinct());
//                 }
//                 ingsLst = ingsLst.Distinct().ToList();
//                 List<int> listAlergyint = new List<int>();
//                 foreach (var item1 in ingsLst)
//                 {
//                     if (await _dietAlergyrepository.TableNoTracking.AnyAsync(a => a.IngredientAlergyId == item1))
//                     {
//                         listAlergyint.Add(_dietAlergyrepository.TableNoTracking.Where(a => a.IngredientAlergyId == item1)
//                             .FirstAsync().Result.MainAlergyId);
//                     }
//                 }
//                 listAlergyint = listAlergyint.Distinct().ToList();
//                 foreach (var item in listAlergyint)
//                 {
//                     DietPackAlerge dietPackAlerge = new DietPackAlerge
//                     {
//                         DietPackId = dietPackbase.Id,
//                         IngredientId = item
//                     };
//                     dietPackAlerges.Add(dietPackAlerge);
//                 }
//
//                 await _alergyRepository.AddRangeAsync(dietPackAlerges, cancellationToken);
//             }
//             #endregion
//
//
//             #region country
//             List<DietPackCountry> dietPackCountriesLst = new List<DietPackCountry>();
//
//             if (request.PackCountries != null)
//             {
//                 if (dietPackbase.PackCountries != null)
//                 {
//                     await _countryrepository.DeleteRangeAsync(dietPackbase.PackCountries, cancellationToken);
//                 }
//
//                 foreach (var item in request.PackCountries)
//                 {
//                     DietPackCountry dietpackcountry = new DietPackCountry
//                     {
//                         DietPack = dietPackbase,
//                         CountryId = item
//                     };
//                     dietPackCountriesLst.Add(dietpackcountry);
//                 }
//             }
//             await _countryrepository.AddRangeAsync(dietPackCountriesLst, cancellationToken);
//
//             #endregion
//
//
//             #region SpecialDisease
//             List<DietPackSpecialDisease> dietpackspecialdiseaseLst = new List<DietPackSpecialDisease>();
//             if (request.DietPackSpecialDiseases.Count() > 0)
//             {
//                 if (dietPackbase.DietPackSpecialDiseases.Count() > 0)
//                 {
//                     await _specialdiseasepackrepository.DeleteRangeAsync(dietPackbase.DietPackSpecialDiseases, cancellationToken);
//                 }
//                 foreach (var item in request.DietPackSpecialDiseases)
//                 {
//                     DietPackSpecialDisease specialdisease = new DietPackSpecialDisease
//                     {
//                         DietPack = dietPackbase,
//                         SpecialDisease = item
//                     };
//                     dietpackspecialdiseaseLst.Add(specialdisease);
//                 }
//                 await _specialdiseasepackrepository.AddRangeAsync(dietpackspecialdiseaseLst, cancellationToken);
//             }
//
//             #endregion
//
//
//             #region DietPackFood
//             List<DietPackFood> dietpackfoodLst = new List<DietPackFood>();
//             {
//                 if (dietPackbase.DietPackFoods.Count() > 0)
//                 {
//                     await _dietpackfoodrepository.DeleteRangeAsync(dietPackbase.DietPackFoods, cancellationToken);
//                 }
//                 foreach (var item in request.DietPackFoods)
//                 {
//                     DietPackFood dietpackfood = new DietPackFood
//                     {
//                         DietPack = dietPackbase,
//                         FoodId = item.FoodId,
//                         MeasureUnitId = item.MeasureUnitId,
//                         Value = item.Value
//                     };
//                     dietpackfoodLst.Add(dietpackfood);
//                 }
//             }
//
//             await _dietpackfoodrepository.AddRangeAsync(dietpackfoodLst, cancellationToken);
//
//             #endregion
//
//
//
//             #region Redis
//             //var dietPackredis = await _dietpackrepository.TableNoTracking
//             //            .Include(d => d.DietPackAlerges)
//             //            .Include(d => d.DietPackFoods).ThenInclude(f => f.Food).ThenInclude(t => t.TranslationName)
//             //            .Include(d => d.DietPackFoods).ThenInclude(f => f.MeasureUnit).ThenInclude(t => t.Translation)
//             //            .Include(d => d.DietPackSpecialDiseases)
//             //            .Include(d => d.PackCountries)
//             //            .Include(d => d.DietCategory)
//             //.Where(m => m.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
//             //List<DietPackFood> DietPackFoods = dietPackbase.DietPackFoods.Select(a => new DietPackFood
//             //{
//             //    FoodId = a.FoodId,
//             //    Food = new Domain.Entities.Food.Food
//             //    {
//             //        Id = a.FoodId,
//             //        TranslationName = new Domain.Entities.Translation.Translation
//             //        {
//             //            Arabic = a.Food.TranslationName.Arabic,
//             //            Persian = a.Food.TranslationName.Persian,
//             //            English = a.Food.TranslationName.English,
//             //            Id = a.Food.TranslationName.Id,
//             //        }
//             //    },
//             //    MeasureUnit = new Domain.Entities.MeasureUnit.MeasureUnit
//             //    {
//             //        Id = a.MeasureUnitId,
//             //        Translation = new Domain.Entities.Translation.Translation
//             //        {
//             //            Arabic = a.MeasureUnit.Translation.Arabic,
//             //            Persian = a.MeasureUnit.Translation.Persian,
//             //            English = a.MeasureUnit.Translation.English,
//             //            Id = a.Food.TranslationName.Id,
//             //        }
//             //    },
//             //    MeasureUnitId = a.MeasureUnitId,
//             //    Id = a.Id,
//             //    Value = a.Value
//             //}).ToList();
//             //dietPackbase.DietPackFoods = DietPackFoods;
//             //dietPackbase.NameId = dietPackredis.NameId;
//             //await _repositoryRedis.UpdateAsync($"DietPack_{request.Id}", dietPackbase);
//
//
//             #endregion
//
//
//             #endregion
//
//
//             #region AddChilds
//
//             var macroNut = CalculateMacroNut.CalculateMacroNutByBodyType(request.CaloriValue,
//                       (int)request.BodyType, (int)request.FoodMeal);
//
//             for (int c = 1020; c < 3400; c += 40)
//             {
//                 var x = c / macroNut.Calorie;
//
//                 //List<double> NutrientArray = new List<double>();
//                 double[] NutrientArray = new double[34];
//                 int i = 0;
//                 foreach (var item in request.NutrientValue)
//                 {
//                     var multi = item * x;
//                     NutrientArray[i] = multi;
//                     i++;
//                 }
//
//                 //NutrientArray[0] = macroNut.Fat;
//                 //NutrientArray[9] = macroNut.Protein;
//                 //NutrientArray[31] = macroNut.Carbohydrate;
//
//                 List<double> NutrientArraylist = NutrientArray.OfType<double>().ToList();
//
//                 DietPack dietPack = new DietPack();
//                 dietPack.NameId = request.NameId;
//                 dietPack.DietPackState = DietPackState.WatingForEdit;
//                 dietPack.BodyType = request.BodyType;
//                 dietPack.CaloriValue = request.CaloriValue * x;
//                 //var cat = await _categoryrepository.Table
//                 //    .Where(a => a.Id == request.DietCategory).FirstAsync();
//                 dietPack.DietCategory = cat;
//                 dietPack.FoodMeal = request.FoodMeal;
//                 dietPack.Tag = request.Tag;
//                 dietPack.TagArEn = request.TagArEn;
//                 dietPack.ParentId = dietPackbase.Id;
//                 dietPack.NutrientValue = StringConvertor.DoubleToString(NutrientArraylist);
//                 var dietpackid = await _dietpackrepository.AddAsync(dietPack, cancellationToken);
//
//                 #region Alergy
//
//                 if (request.DietPackFoods.Count > 0)
//                 {
//                     List<DietPackAlerge> dietPackAlergeslst = new List<DietPackAlerge>();
//                     List<int> ingsLst = new List<int>();
//                     foreach (var item in request.DietPackFoods)
//                     {
//                         var ingList = await _foodIngrepository.TableNoTracking
//                             .Where(a => a.FoodId == item.FoodId).Select(a => a.IngredientId).ToListAsync();
//                         ingsLst.AddRange(ingList.Distinct());
//                     }
//                     ingsLst = ingsLst.Distinct().ToList();
//                     List<int> listAlergyint = new List<int>();
//                     foreach (var item1 in ingsLst)
//                     {
//                         if (await _dietAlergyrepository.TableNoTracking.AnyAsync(a => a.IngredientAlergyId == item1))
//                         {
//                             listAlergyint.Add(_dietAlergyrepository.TableNoTracking.Where(a => a.IngredientAlergyId == item1)
//                                 .FirstAsync().Result.MainAlergyId);
//                         }
//                     }
//                     listAlergyint = listAlergyint.Distinct().ToList();
//                     foreach (var item in listAlergyint)
//                     {
//                         DietPackAlerge dietPackAlerge = new DietPackAlerge
//                         {
//                             DietPackId = dietpackid,
//                             IngredientId = item
//                         };
//                         dietPackAlergeslst.Add(dietPackAlerge);
//                     }
//
//                     await _alergyRepository.AddRangeAsync(dietPackAlergeslst, cancellationToken);
//                 }
//                 #endregion
//
//
//                 #region Country
//                 if (request.PackCountries.Count > 0)
//                 {
//                     List<DietPackCountry> dietPackCountriesList = new List<DietPackCountry>();
//                     foreach (var item in request.PackCountries)
//                     {
//                         DietPackCountry dietpackcountry = new DietPackCountry
//                         {
//                             DietPack = dietPack,
//                             CountryId = item
//                         };
//                         dietPackCountriesList.Add(dietpackcountry);
//                     }
//
//                     await _countryrepository.AddRangeAsync(dietPackCountriesList, cancellationToken);
//                 }
//                 #endregion
//
//
//                 #region Disease
//                 if (request.DietPackSpecialDiseases.Count > 0)
//                 {
//                     List<DietPackSpecialDisease> dietpackspecialdiseaseList = new List<DietPackSpecialDisease>();
//                     foreach (var item in request.DietPackSpecialDiseases)
//                     {
//                         DietPackSpecialDisease specialdisease = new DietPackSpecialDisease
//                         {
//                             DietPack = dietPack,
//                             SpecialDisease = item
//                         };
//                         dietpackspecialdiseaseList.Add(specialdisease);
//                     }
//
//                     await _specialdiseasepackrepository.AddRangeAsync(dietpackspecialdiseaseList, cancellationToken);
//                 }
//                 #endregion
//
//                 #region Dietpackfood
//
//                 if (request.DietPackFoods.Count > 0)
//                 {
//                     //List<DietPackFood> dietpackfoodList = new List<DietPackFood>();
//                     foreach (var item in request.DietPackFoods)
//                     {
//                         DietPackFood dietpackfood = new DietPackFood
//                         {
//                             DietPack = dietPack,
//                             FoodId = item.FoodId,
//                             MeasureUnitId = item.MeasureUnitId,
//                             Value = item.Value * x
//
//                         };
//                         //dietpackfoodList.Add(dietpackfood);
//                         await _dietpackfoodrepository.AddAsync(dietpackfood, cancellationToken);
//                     }
//
//                     //await _dietpackfoodrepository.AddRangeAsync(dietpackfoodList, cancellationToken);
//                 }
//                 #endregion
//
//
//
//                 #region Redis
//                 //    var dietPackredis = await _dietpackrepository.TableNoTracking
//                 //             .Include(a => a.Translation)
//                 //            .Include(d => d.DietPackAlerges)
//                 //            .Include(d => d.DietPackFoods).ThenInclude(f => f.Food).ThenInclude(t => t.TranslationName)
//                 //            .Include(d => d.DietPackFoods).ThenInclude(f => f.MeasureUnit).ThenInclude(t => t.Translation)
//                 //            .Include(d => d.DietPackSpecialDiseases)
//                 //            .Include(d => d.PackCountries)
//                 //            .Include(d => d.DietCategory)
//                 //.Where(m => m.Id == dietpackid).FirstOrDefaultAsync(cancellationToken);
//                 //    _dietpackrepository.Detach(dietPackredis);
//
//                 //    List<DietPackFood> DietPackFoods = dietPack.DietPackFoods.Select(a => new DietPackFood
//                 //    {
//                 //        FoodId = a.FoodId,
//                 //        Food = new Domain.Entities.Food.Food
//                 //        {
//                 //            Id = a.FoodId,
//                 //            TranslationName = new Domain.Entities.Translation.Translation
//                 //            {
//                 //                Arabic = a.Food.TranslationName.Arabic,
//                 //                Persian = a.Food.TranslationName.Persian,
//                 //                English = a.Food.TranslationName.English,
//                 //                Id = a.Food.TranslationName.Id,
//                 //            }
//                 //        },
//                 //        MeasureUnit = new Domain.Entities.MeasureUnit.MeasureUnit
//                 //        {
//                 //            Id = a.MeasureUnitId,
//                 //            Translation = new Domain.Entities.Translation.Translation
//                 //            {
//                 //                Arabic = a.MeasureUnit.Translation.Arabic,
//                 //                Persian = a.MeasureUnit.Translation.Persian,
//                 //                English = a.MeasureUnit.Translation.English,
//                 //                Id = a.Food.TranslationName.Id,
//                 //            }
//                 //        },
//                 //        MeasureUnitId = a.MeasureUnitId,
//                 //        Id = a.Id,
//                 //        Value = a.Value
//                 //    }).ToList();
//
//                 //    dietPack.DietPackFoods = DietPackFoods;
//                 //    await _repositoryRedis.UpdateAsync($"DietPack_{dietpackid}", dietPack);
//
//                 #endregion
//             }
//                 #endregion
//
//
//             return dietPackbase;
//
//             }
//         
//     }
// }
