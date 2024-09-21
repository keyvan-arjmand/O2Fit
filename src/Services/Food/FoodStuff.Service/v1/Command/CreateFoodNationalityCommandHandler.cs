using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
     public class CreateFoodNationalityCommandHandler : IRequestHandler<CreateFoodNationalityCommand, Unit>, IScopedDependency
     {
         private readonly IRepository<FoodNationality> _repository;

         public CreateFoodNationalityCommandHandler(IRepository<FoodNationality> repository)
         {
             _repository = repository;
         }

         public async Task<Unit> Handle(CreateFoodNationalityCommand request, CancellationToken cancellationToken)
         {
             foreach (var nationalityId in request.NationalityIds)
             {
                 var foodNationality = new FoodNationality
                 {
                     NationalityId = nationalityId,
                     FoodId = request.FoodId
                 };

                 await _repository.AddAsync(foodNationality,cancellationToken);

                
             }

             return Unit.Value;
        }
     }
 }
