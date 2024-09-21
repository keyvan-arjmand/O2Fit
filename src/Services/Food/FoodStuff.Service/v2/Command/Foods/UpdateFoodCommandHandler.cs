using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v2.Command.Foods
{
    public class UpdateFoodCommandHandler: IRequestHandler<UpdateFoodCommand , Unit>, IScopedDependency
    {
        private readonly IRepository<FoodStuff.Domain.Entities.Food.Food> _repository;

        public UpdateFoodCommandHandler(IRepository<Food> repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}