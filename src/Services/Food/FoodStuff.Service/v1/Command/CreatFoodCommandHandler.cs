using Common;
using Common.Exceptions;
using Common.Utilities;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command
{
    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, int>, ITransientDependency
    {
        private readonly IFileService _fileService;
        private readonly IFoodRepository _foodRepository;

        public CreateFoodCommandHandler(IFoodRepository foodRepository
            , IFileService fileService)
        {
            _foodRepository = foodRepository;
            _fileService = fileService;
        }

        public async Task<int> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {

            if (!string.IsNullOrEmpty(request.ImageUri))
            {
                var imageUri = _fileService.AddImage(request.ImageUri, "FoodImage", request.FoodCode.ToString());
                request.ImageUri = imageUri;
            }

            if (!string.IsNullOrEmpty(request.ImageThumb))
            {
                var imageThumb = _fileService.AddImage(request.ImageThumb, "FoodThumb", request.FoodCode.ToString() + "-2");
                request.ImageThumb = imageThumb;
            }

            var foodNutrients = new List<double>();
            request.RecipeId = request.RecipeId > 0 ? request.RecipeId : null;
            if (request.FoodType != FoodType.Supermarket)
            {
                var foodFactors = Formula.Formula.FoodWeightsCalculation(new FoodParameters
                {
                    foodWeight = request.SumWeight,
                    BakingRatio = (request.BakingType).ToDescription().ToDouble(),
                    BakingTimeInMinute = request.BakingTime.TotalMinutes,
                    foodNutrients = request.IngredientCalculate
                });

                if (foodFactors.AfterBaking - foodFactors.DryIngredient <= 0)
                    throw new AppException(ApiResultStatusCode.BadRequest, "Your food is burnt");

                request.WeightBeforBaking = request.SumWeight;
                request.EvaporatedWater = foodFactors.EvaporatedWater;
                request.WeightAfterBaking = foodFactors.AfterBaking;
                request.DryIngredient = foodFactors.DryIngredient;
                var nutrients = request.IngredientCalculate;

                if ((int)request.FoodType != 3 && foodFactors.AfterBaking > 0)
                {
                    foreach (var nut in nutrients) foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);
                    foodNutrients[1] = (foodFactors.AfterBaking - foodFactors.DryIngredient) * 100 /
                                       foodFactors.AfterBaking;
                    request.NutrientValue = StringConvertor.DoubleToString(foodNutrients);
                }
            }




            request.IsUpdate = false;
            request.BrandId = request.BrandId > 0 ? request.BrandId : null;

            var food = new Food
            {
                BakingTime = request.BakingTime,
                BakingType = request.BakingType,
                BarcodeGs1 = request.BarcodeGs1,
                BarcodeNational = request.BarcodeNational,
                BrandId = request.BrandId,
                DryIngredient = request.DryIngredient,
                EvaporatedWater = request.EvaporatedWater,
                FoodCode = request.FoodCode,
                FoodType = request.FoodType,
                ImageThumb = request.ImageThumb,
                ImageUri = request.ImageUri,
                IsActive = request.IsActive,
                IsDelete = false,
                IsIngredient = request.IsIngredient,
                IsUpdate = request.IsUpdate,
                NameId = request.NameId,
                NutrientValue = request.NutrientValue,
                PersonCount = request.PersonCount,
                RecipeId = request.RecipeId,
                Tag = request.Tag,
                TagArEn = request.TagArEn,
                Version = request.Version,
                WeightAfterBaking = request.WeightAfterBaking,
                WeightBeforBaking = request.WeightBeforBaking,
                GI = request.GI,
                UseInDiet = request.UseInDiet,
                DefaultMeasureUnitId = request.DefaultMeasureUnitId,
                FoodMeals = request.FoodMeals,
                IsRecipe = request.IsRecipe,
            };
            int foodId = await _foodRepository.AddAsync(food, cancellationToken);


            return foodId;
        }
    }
}