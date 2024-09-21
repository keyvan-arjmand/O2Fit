using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Command.Foods
{
    
    public class SoftDeleteFoodByIdCommandHandler: IRequestHandler<SoftDeleteFoodByIdCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Food> _repository;

        public SoftDeleteFoodByIdCommandHandler(IRepository<Food> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteFoodByIdCommand request, CancellationToken cancellationToken)
        {
            var food = await _repository.Table.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (food == null)
                throw new AppException(ApiResultStatusCode.ServerError);

            food.IsDelete = true;

            await _repository.UpdateAsync(food, cancellationToken);

            return Unit.Value;
        }
    }
}