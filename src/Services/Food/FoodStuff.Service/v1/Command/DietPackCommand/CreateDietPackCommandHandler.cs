using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command.DietPackCommand
{
    public class CreateDietPackCommandHandler :
        IRequestHandler<CreateDietPackCommand, Unit>, IScopedDependency
    {
        private readonly IDietPackRepository _dietpackrepository;
        private readonly IRepository<DietPackAlerge> _alergyRepository;
        private readonly IRepository<DietPackNationality> _dietPackNationalityRepository;
        private readonly IRepository<FoodIngredient> _foodIngrepository;
        private readonly IRepository<DietPackFood> _dietpackfoodrepository;
        private readonly IRepository<DietPackDietCategory> _dietpackDietCategoryRepository;
        private readonly IRepository<FoodSpecialDisease> _foodSpecialDiseaseRepository;
        private readonly IRepository<DietPackSpecialDisease> _dietpackSpecialDiseaseRepository;
        private readonly IRepository<IngredientAllergy> _ingredientAllergyRepository;

        private readonly IRepository<IngredientAllergyCategoryIngredientAllergy>
            _ingredientAllergyCategoryIngredientAllergyRepository;
        public CreateDietPackCommandHandler(IDietPackRepository dietpackrepository,
        IRepository<DietPackAlerge> alergyRepository,
        IRepository<DietPackNationality> dietPackNationalityRepository,
        IRepository<DietPackFood> dietpackfoodrepository,
        IRepository<FoodIngredient> foodIngrepository,
        IRepository<DietPackDietCategory> dietpackDietCategoryRepository,
        IRepository<FoodSpecialDisease> foodSpecialDiseaseRepository,
        IRepository<DietPackSpecialDisease> dietpackSpecialDiseaseRepository,
        IRepository<IngredientAllergy> ingredientAllergyRepository, IRepository<IngredientAllergyCategoryIngredientAllergy> ingredientAllergyCategoryIngredientAllergyRepository)
        {
            _alergyRepository = alergyRepository;
            _dietpackrepository = dietpackrepository;
            _dietPackNationalityRepository = dietPackNationalityRepository;
            _dietpackfoodrepository = dietpackfoodrepository;
            _foodIngrepository = foodIngrepository;
            _dietpackDietCategoryRepository = dietpackDietCategoryRepository;
            _foodSpecialDiseaseRepository = foodSpecialDiseaseRepository;
            _dietpackSpecialDiseaseRepository = dietpackSpecialDiseaseRepository;
            _ingredientAllergyRepository = ingredientAllergyRepository;
            _ingredientAllergyCategoryIngredientAllergyRepository = ingredientAllergyCategoryIngredientAllergyRepository;
        }

        public async Task<Unit> Handle(CreateDietPackCommand request, CancellationToken cancellationToken)
        {


            Domain.Entities.Diet.DietPack dietPack = new Domain.Entities.Diet.DietPack
            {
                CaloriValue = request.CalorieValue,
                CategoryId = request.CategoryId,
                DailyCalorie = request.DailyCalorie,
                FoodMeal = request.FoodMeal,
                IsActive = request.IsActive,
                NameId = request.NameId,
                NutrientValue = StringConvertor.DoubleToString(request.NutrientValue),
            };

            var dietPackId = await _dietpackrepository.AddAsync(dietPack, cancellationToken);


            if (request.DietPackFoods.Any())
            {
                List<DietPackAlerge> dietPackAlerges = new List<DietPackAlerge>();
                List<int> ingsLst = new List<int>();

                foreach (var item in request.DietPackFoods)
                {
                    var ingList = await _foodIngrepository.TableNoTracking
                        .Where(a => a.FoodId == item.FoodId)
                        .Select(a => a.IngredientId).ToListAsync(cancellationToken: cancellationToken);

                    ingsLst.AddRange(ingList.Distinct());
                }
                ingsLst = ingsLst.Distinct().ToList();

                List<int> listAlergyint = new List<int>();

                foreach (var item1 in ingsLst)
                {
                    if (await _ingredientAllergyRepository.TableNoTracking.AnyAsync(a => a.IngredientId == item1))
                    {
                        listAlergyint.Add(await _ingredientAllergyRepository.TableNoTracking.Where(a => a.IngredientId == item1)
                            .Select(i => i.IngredientId).FirstAsync());
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
                        DietPackId = dietPackId,
                        IngredientId = item,
                        IngredientAllergyCategoryId = ingredientAllergyCategories.IngredientAllergyCategoryId
                    };
                    dietPackAlerges.Add(dietPackAlerge);
                }

                await _alergyRepository.AddRangeAsync(dietPackAlerges, cancellationToken);
            }



            if (request.NationalityIds.Any())
            {
                List<DietPackNationality> dietPackNationalities = new List<DietPackNationality>();
                foreach (var item in request.NationalityIds)
                {
                    DietPackNationality dietPackNationality = new DietPackNationality
                    {
                        DietPackId = dietPackId,
                        NationalityId = item
                    };
                    dietPackNationalities.Add(dietPackNationality);
                }

                await _dietPackNationalityRepository.AddRangeAsync(dietPackNationalities, cancellationToken);
            }


            if (request.DietPackFoods.Count > 0)
            {
                List<DietPackFood> dietPackFoods = new List<DietPackFood>();
                foreach (var item in request.DietPackFoods)
                {
                    DietPackFood dietPackFood = new DietPackFood
                    {
                        DietPackId = dietPackId,
                        FoodId = item.FoodId,
                        MeasureUnitId = item.MeasureUnitId,
                        Value = item.Value,
                        Calorie = item.Calorie,
                        CategoryChildId = item.CategoryChildId,
                        NutrientValue = StringConvertor.DoubleToString(item.NutrientValue)
                    };
                    dietPackFoods.Add(dietPackFood);
                }

                await _dietpackfoodrepository.AddRangeAsync(dietPackFoods, cancellationToken);


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
                            DietPackId = dietPackId,
                            SpecialDisease = item
                        };

                        dietPackSpecialDiseases.Add(dietPackSpecialDisease);
                    }

                    await _dietpackSpecialDiseaseRepository.AddRangeAsync(dietPackSpecialDiseases, cancellationToken);
                }
            }


            if (request.DietCategoryIds.Any())
            {
                List<DietPackDietCategory> dietPackDietCategories = new List<DietPackDietCategory>();

                foreach (var dietCategoryId in request.DietCategoryIds)
                {
                    DietPackDietCategory dietPackDietCategory = new DietPackDietCategory
                    {
                        DietCategoryId = dietCategoryId,
                        DietPackId = dietPackId
                    };
                    dietPackDietCategories.Add(dietPackDietCategory);
                }

                await _dietpackDietCategoryRepository.AddRangeAsync(dietPackDietCategories, cancellationToken);
            }




            return Unit.Value;
        }

    }
}
