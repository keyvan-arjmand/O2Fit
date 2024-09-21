using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command.DietPackCommand
{
    public class EditDietPackCommandHandler : IRequestHandler<EditDietPackCommand, Domain.Entities.Diet.DietPack>,
        ITransientDependency
    {
        private readonly IDietPackRepository _dietpackrepository;
        private readonly IRepository<DietPackNationality> _dietPackNationalityRepository;
        private readonly IRepository<DietCategory> _categoryrepository;
        private readonly IRepository<FoodIngredient> _foodIngrepository;
        private readonly IRepository<DietPackFood> _dietpackfoodrepository;
        private readonly IRepository<DietPackAlerge> _dietPackAlergeRepository;
        private readonly IRepositoryRedis<Domain.Entities.Diet.DietPack> _repositoryRedis;
        private readonly IRepository<DietPackDietCategory> _dietPackDietCategoryRepository;
        private readonly IRepository<DietPackSpecialDisease> _dietPackSpecialDiseaseRepository;
        private readonly IRepository<FoodSpecialDisease> _foodSpecialDiseaseRepository;
        private readonly IRepository<IngredientAllergy> _ingredientAllergyRepository;
        private readonly IRepository<IngredientAllergyCategoryIngredientAllergy>
            _ingredientAllergyCategoryIngredientAllergyRepository;

        public EditDietPackCommandHandler(IDietPackRepository dietpackrepository,
        IRepository<DietPackNationality> dietPackNationalityRepository,
        IRepository<DietCategory> categoryrepository,
        IRepository<DietPackFood> dietpackfoodrepository,
        IRepository<DietPackDietCategory> dietPackDietCategoryRepository,
            IRepository<FoodIngredient> foodIngrepository,
            IRepository<DietPackAlerge> dietPackAlergeRepository,
            IRepositoryRedis<Domain.Entities.Diet.DietPack> repositoryRedis,
            IRepository<DietPackSpecialDisease> dietPackSpecialDiseaseRepository,
            IRepository<FoodSpecialDisease> foodSpecialDiseaseRepository,
            IRepository<IngredientAllergy> ingredientAllergyRepository, IRepository<IngredientAllergyCategoryIngredientAllergy> ingredientAllergyCategoryIngredientAllergyRepository)
        {
            _dietpackrepository = dietpackrepository;
            _dietPackNationalityRepository = dietPackNationalityRepository;
            _categoryrepository = categoryrepository;
            _dietpackfoodrepository = dietpackfoodrepository;
            _foodIngrepository = foodIngrepository;
            _dietPackAlergeRepository = dietPackAlergeRepository;
            _repositoryRedis = repositoryRedis;
            _dietPackDietCategoryRepository = dietPackDietCategoryRepository;
            _dietPackSpecialDiseaseRepository = dietPackSpecialDiseaseRepository;
            _foodSpecialDiseaseRepository = foodSpecialDiseaseRepository;
            _ingredientAllergyRepository = ingredientAllergyRepository;
            _ingredientAllergyCategoryIngredientAllergyRepository = ingredientAllergyCategoryIngredientAllergyRepository;
        }

        public async Task<Domain.Entities.Diet.DietPack> Handle(EditDietPackCommand request, CancellationToken cancellationToken)
        {
            try
            {

                Domain.Entities.Diet.DietPack dietPack = await _dietpackrepository
                    .Table.Include(a => a.DietPackAlerges)
                    .Include(c => c.DietPackDietCategories)
                    .Include(d => d.DietPackNationalities)
                    .Include(a => a.DietPackFoods)
                    .Include(s => s.DietPackSpecialDiseases)
                    .Where(p => p.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);



                dietPack.CaloriValue = request.CalorieValue;
                dietPack.FoodMeal = request.FoodMeal;
                dietPack.IsActive = request.IsActive;
                dietPack.NutrientValue = StringConvertor.DoubleToString(request.NutrientValue);
                dietPack.NameId = request.NameId;
                dietPack.DailyCalorie = request.DailyCalorie;

                await _dietpackrepository.UpdateAsync(dietPack, cancellationToken);

                #region Alergy
                List<DietPackAlerge> dietPackAlerges = new List<DietPackAlerge>();
                if (request.DietPackFoods.Count > 0)
                {
                    if (dietPack.DietPackAlerges.Any())
                    {
                        await _dietPackAlergeRepository.DeleteRangeAsync(dietPack.DietPackAlerges, cancellationToken);
                    }

                    List<int> ingsLst = new List<int>();
                    foreach (var item in request.DietPackFoods)
                    {
                        var ingList = await _foodIngrepository.TableNoTracking
                            .Where(a => a.FoodId == item.FoodId).Select(a => a.IngredientId).ToListAsync();
                        ingsLst.AddRange(ingList.Distinct());
                    }
                    ingsLst = ingsLst.Distinct().ToList();
                    List<int> listAlergyint = new List<int>();
                    foreach (var item1 in ingsLst)
                    {
                        if (await _ingredientAllergyRepository.TableNoTracking.AnyAsync(a => a.IngredientId == item1))
                        {
                            listAlergyint.Add(await _ingredientAllergyRepository.TableNoTracking.Where(a => a.IngredientId == item1)
                                .Select(i => i.IngredientId).FirstAsync(cancellationToken));
                        }
                    }
                    listAlergyint = listAlergyint.Distinct().ToList();
                    foreach (var item in listAlergyint)
                    {
                        var ingredientAllergyCategories = await _ingredientAllergyCategoryIngredientAllergyRepository
                            .TableNoTracking
                            .Include(x => x.IngredientAllergy)
                            .FirstOrDefaultAsync(x => x.IngredientAllergy.IngredientId == item, cancellationToken);

                        DietPackAlerge dietPackAlerge = new DietPackAlerge
                        {
                            DietPackId = dietPack.Id,
                            IngredientId = item,
                            IngredientAllergyCategoryId = ingredientAllergyCategories.IngredientAllergyCategoryId
                        };
                        dietPackAlerges.Add(dietPackAlerge);
                    }

                    await _dietPackAlergeRepository.AddRangeAsync(dietPackAlerges, cancellationToken);
                }
                #endregion






                if (dietPack.DietPackNationalities.Any())
                {
                    await _dietPackNationalityRepository.DeleteRangeAsync(dietPack.DietPackNationalities,
                        cancellationToken);
                }

                if (request.NationalityIds.Any())
                {
                    List<DietPackNationality> dietPackNationalities = new List<DietPackNationality>();

                    foreach (var item in request.NationalityIds)
                    {
                        DietPackNationality dietPackNationality = new DietPackNationality
                        {
                            DietPackId = dietPack.Id,
                            NationalityId = item
                        };
                        dietPackNationalities.Add(dietPackNationality);
                    }

                    _dietPackNationalityRepository.AddRange(dietPackNationalities);
                }



                #region DietPackFood


                if (dietPack.DietPackFoods.Any())
                {
                    await _dietpackfoodrepository.DeleteRangeAsync(dietPack.DietPackFoods, cancellationToken);
                }

                if (request.DietPackFoods.Any())
                {
                    List<DietPackFood> dietPackFoods = new List<DietPackFood>();

                    foreach (var item in request.DietPackFoods)
                    {
                        DietPackFood dietpackfood = new DietPackFood
                        {
                            DietPack = dietPack,
                            FoodId = item.FoodId,
                            MeasureUnitId = item.MeasureUnitId,
                            Value = item.Value,
                            NutrientValue = StringConvertor.DoubleToString(item.NutrientValue),
                            Calorie = item.Calorie,
                            CategoryChildId = item.CategoryChildId,
                            DietPackId = dietPack.Id,
                        };
                        dietPackFoods.Add(dietpackfood);
                    }

                    await _dietpackfoodrepository.AddRangeAsync(dietPackFoods, cancellationToken);
                }


                #endregion



                if (dietPack.DietPackDietCategories.Any())
                {
                    await _dietPackDietCategoryRepository.DeleteRangeAsync(dietPack.DietPackDietCategories,
                        cancellationToken);
                }

                if (request.DietCategoryIds.Any())
                {
                    List<DietPackDietCategory> dietPackDietCategories = new List<DietPackDietCategory>();

                    foreach (var dietCategoryId in request.DietCategoryIds)
                    {
                        DietPackDietCategory dietPackDietCategory = new DietPackDietCategory
                        {
                            DietCategoryId = dietCategoryId,
                            DietPackId = dietPack.Id
                        };

                        dietPackDietCategories.Add(dietPackDietCategory);
                    }

                    await _dietPackDietCategoryRepository.AddRangeAsync(dietPackDietCategories, cancellationToken);
                }

                if (dietPack.DietPackSpecialDiseases.Any())
                {
                    await _dietPackSpecialDiseaseRepository.DeleteRangeAsync(dietPack.DietPackSpecialDiseases,
                        cancellationToken);
                }

                var foodSpecialDiseaselist = new List<List<SpecialDisease>>();

                foreach (var item in request.DietPackFoods)
                {
                    List<SpecialDisease> specialDisease = await _foodSpecialDiseaseRepository
                        .TableNoTracking.Where(fs => fs.FoodId == item.FoodId)
                    .Select(fs => fs.SpecialDisease).ToListAsync(cancellationToken);

                    foodSpecialDiseaselist.Add(new List<SpecialDisease>(specialDisease));

                }


                List<SpecialDisease> result = new List<SpecialDisease>();

                if (request.DietPackFoods.Count == 1 && foodSpecialDiseaselist.Any())
                {
                    result = foodSpecialDiseaselist.First();
                }
                else
                {
                    for (int i = 0; i < foodSpecialDiseaselist.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            result = foodSpecialDiseaselist[i + 1].FindAll(x => foodSpecialDiseaselist[i].Contains(x));
                        }
                        else
                        {
                            result = foodSpecialDiseaselist[i + 1].FindAll(x => result.Contains(x));
                        }
                    }
                }



                var dietPackSpecialDiseases = new List<DietPackSpecialDisease>();
                if (result.Any())
                {
                    foreach (var item in result)
                    {
                        DietPackSpecialDisease dietPackSpecialDisease = new DietPackSpecialDisease
                        {
                            DietPackId = dietPack.Id,
                            SpecialDisease = item
                        };

                        dietPackSpecialDiseases.Add(dietPackSpecialDisease);
                    }

                    await _dietPackSpecialDiseaseRepository.AddRangeAsync(dietPackSpecialDiseases, cancellationToken);
                }

                return dietPack;

            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }
        }

    }
}
