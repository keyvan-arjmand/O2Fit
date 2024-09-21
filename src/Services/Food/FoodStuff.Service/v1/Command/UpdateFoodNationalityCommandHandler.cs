using System.Threading;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using System;
using Common;
using Common.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodNationalityCommandHandler : IRequestHandler<UpdateFoodNationalityCommand, Unit>, ITransientDependency
    {
        private readonly IRepository<FoodNationality> _foodNationalityRepository;

        public UpdateFoodNationalityCommandHandler(IRepository<FoodNationality> foodNationalityRepository)
        {
            _foodNationalityRepository = foodNationalityRepository;
        }

        public async Task<Unit> Handle(UpdateFoodNationalityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.FoodNationalities.Any())
                {
                    foreach (var foodNationality in request.FoodNationalities)
                    {
                        _foodNationalityRepository.Detach(foodNationality);
                    }
                }


                var foodNationalities = _foodNationalityRepository.Table
                    .Where(f => f.FoodId == request.FoodId).ToList();
                if (foodNationalities.Any())
                {
                    foreach (var foodNationality in foodNationalities)
                    {
                        await _foodNationalityRepository.DeleteAsync(foodNationality, cancellationToken);
                    }

                }

                if (request.NationalityIds.Any())
                {
                    foreach (var nationalityId in request.NationalityIds)
                    {
                        var foodNationality = new FoodNationality
                        {
                            NationalityId = nationalityId,
                            FoodId = request.FoodId
                        };

                        await _foodNationalityRepository.AddAsync(foodNationality, cancellationToken);
                    }
                }
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }

        }
    }
}
