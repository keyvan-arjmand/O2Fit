using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class CreateFoodSpecialDiseasesCommandHandler : IRequestHandler<CreateFoodSpecialDiseasesCommand, Unit> , ITransientDependency
    {
        private readonly IRepository<FoodSpecialDisease> _foodSpecialDiseaseRepository;

        public CreateFoodSpecialDiseasesCommandHandler(IRepository<FoodSpecialDisease> foodSpecialDiseaseRepository)
        {
            _foodSpecialDiseaseRepository = foodSpecialDiseaseRepository;
        }

        public async Task<Unit> Handle(CreateFoodSpecialDiseasesCommand request, CancellationToken cancellationToken)
        {
            List<FoodSpecialDisease> foodSpecialDiseases = new List<FoodSpecialDisease>();
            foreach (var item in request.FoodSpecialDiseases)
            {
                var foodSpecialDisease = new FoodSpecialDisease
                {
                    FoodId = request.FoodId,
                    SpecialDisease = (Domain.Enum.SpecialDisease)item
                };

                foodSpecialDiseases.Add(foodSpecialDisease);
            }

            await _foodSpecialDiseaseRepository.AddRangeAsync(foodSpecialDiseases, cancellationToken);

            return Unit.Value;
        }
    }
}