using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.Contracts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query
{
    public class DeleteFoodQueryHandler : IRequestHandler<DeleteFoodQuery, Unit>, IScopedDependency
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IRepository<FoodNationality> _foodNationalityRepository;
        private readonly IRepository<FoodCategory> _foodCategoryRepository;
        private readonly IRepository<FoodDietCategory> _foodDietCategoryRepository;
        private readonly IRepository<FoodSpecialDisease> _foodSpecialDiseaseRepository;
        private readonly IRepository<FoodIngredient> _foodIngredientRepository;
        private readonly IRepository<FoodMeasureUnit> _foodMeasureUnitRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly ITranslationRepository _translationRepository;
        private readonly IRepositoryRedis<Translation> _repositoryRedis;
        private readonly IRepository<UserTrackFood> _userTrackFoodRepository;
        private readonly IRepository<DietPackFood> _dietPackFoodRepository;
        private readonly IRepository<FoodFoodHabit> _foodFoodHabitRepository;
        private readonly IFileService _fileService;

        public DeleteFoodQueryHandler(IFoodRepository foodRepository, IRepository<FoodNationality> foodNationalityRepository, IRepository<FoodCategory> foodCategoryRepository, IRepository<FoodDietCategory> foodDietCategoryRepository, IRepository<FoodSpecialDisease> foodSpecialDiseaseRepository, IRepository<FoodIngredient> foodIngredientRepository, IRepository<FoodMeasureUnit> foodMeasureUnitRepository, IWebHostEnvironment environment, ITranslationRepository translationRepository, IRepositoryRedis<Translation> repositoryRedis, IRepository<UserTrackFood> userTrackFoodRepository, IRepository<DietPackFood> dietPackFoodRepository, IRepository<FoodFoodHabit> foodFoodHabitRepository, IFileService fileService)
        {
            _foodRepository = foodRepository;
            _foodNationalityRepository = foodNationalityRepository;
            _foodCategoryRepository = foodCategoryRepository;
            _foodDietCategoryRepository = foodDietCategoryRepository;
            _foodSpecialDiseaseRepository = foodSpecialDiseaseRepository;
            _foodIngredientRepository = foodIngredientRepository;
            _foodMeasureUnitRepository = foodMeasureUnitRepository;
            _environment = environment;
            _translationRepository = translationRepository;
            _repositoryRedis = repositoryRedis;
            _userTrackFoodRepository = userTrackFoodRepository;
            _dietPackFoodRepository = dietPackFoodRepository;
            _foodFoodHabitRepository = foodFoodHabitRepository;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(DeleteFoodQuery request, CancellationToken cancellationToken)
        {
            bool useInUserTrackFood = await _userTrackFoodRepository.TableNoTracking.AnyAsync(u => u.FoodId == request.Id, cancellationToken: cancellationToken);

            if (useInUserTrackFood)
                throw new AppException(ApiResultStatusCode.BadRequest, " غذا در روزانه ی کاربر استفاده شده است");

            bool useInDietPack = await _dietPackFoodRepository.TableNoTracking
                .AnyAsync(d => d.FoodId == request.Id, cancellationToken);

            if (useInDietPack)
                throw new AppException(ApiResultStatusCode.BadRequest, " غذا در پکیج رژیم استفاده شده است");

            var food = await _foodRepository.GetByIdAsync(cancellationToken, request.Id);


            await _foodSpecialDiseaseRepository.DeleteRangeAsync(food.FoodSpecialDiseases, cancellationToken);
            await _foodCategoryRepository.DeleteRangeAsync(food.FoodCategories, cancellationToken);
            await _foodNationalityRepository.DeleteRangeAsync(food.FoodNationalities, cancellationToken);
            await _foodDietCategoryRepository.DeleteRangeAsync(food.FoodDietCategories, cancellationToken);
            await _foodMeasureUnitRepository.DeleteRangeAsync(food.FoodMeasureUnits, cancellationToken);
            await _foodIngredientRepository.DeleteRangeAsync(food.FoodIngredients, cancellationToken);
            await _foodFoodHabitRepository.DeleteRangeAsync(food.FoodHabits, cancellationToken);


            await _foodRepository.DeleteAsync(food, cancellationToken);


            await _translationRepository.DeleteAsync(food.TranslationName, cancellationToken);


            await _translationRepository.DeleteAsync(food.TranslationRecipe, cancellationToken);

            if (!string.IsNullOrEmpty(food.ImageUri))
            {
                _fileService.RemoveImage(food.ImageUri, "FoodImage");
            }
            if (!string.IsNullOrEmpty(food.ImageThumb))
            {
                _fileService.RemoveImage(food.ImageThumb, "FoodThumb");
            }


            await _repositoryRedis.DeleteAsync($"Translation_Food_{food.Id}");



            return Unit.Value;
        }
    }
}
