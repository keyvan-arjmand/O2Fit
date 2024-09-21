using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodSpecialDiseasesCommandHandler : IRequestHandler<UpdateFoodSpecialDiseasesCommand, Unit>, ITransientDependency
    {
        private readonly IRepository<FoodSpecialDisease> _foodSpecialDiseasesRepository;

        public UpdateFoodSpecialDiseasesCommandHandler(IRepository<FoodSpecialDisease> foodSpecialDiseasesRepository)
        {
            _foodSpecialDiseasesRepository = foodSpecialDiseasesRepository;
        }

        public async Task<Unit> Handle(UpdateFoodSpecialDiseasesCommand request, CancellationToken cancellationToken)
        {
            if (request.OldSpecialDiseases.Any())
            {
                foreach (var oldSpecialDisease in request.OldSpecialDiseases)
                {
                    _foodSpecialDiseasesRepository.Detach(oldSpecialDisease);
                }
            }


            var foodSpecialDiseases = _foodSpecialDiseasesRepository.Table
                .Where(f => f.FoodId == request.FoodId).ToList();
            if (foodSpecialDiseases.Any())
            {
                foreach (var foodSpecialDisease in foodSpecialDiseases)
                {
                    await _foodSpecialDiseasesRepository.DeleteAsync(foodSpecialDisease, cancellationToken);
                }

            }

            if (request.SpecialDiseases.Any())
            {
                foreach (var specialDisease in request.SpecialDiseases)
                {
                    var foodSpecialDisease = new FoodSpecialDisease
                    {
                        SpecialDisease = (Domain.Enum.SpecialDisease)specialDisease,
                        FoodId = request.FoodId
                    };

                    await _foodSpecialDiseasesRepository.AddAsync(foodSpecialDisease, cancellationToken);
                }
            }
            return Unit.Value;
        }
    }
}
