using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Command.Foods
{
    public class UpdateRecipeCategoryIdCommandHandler : IRequestHandler<UpdateRecipeCategoryIdCommand>, IScopedDependency
    {
        private readonly IRepository<FoodStuff.Domain.Entities.Food.Food> _repository;

        public UpdateRecipeCategoryIdCommandHandler(IRepository<Food> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateRecipeCategoryIdCommand request, CancellationToken cancellationToken)
        {
            var food = await _repository.Table.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            food.RecipeCategoryId = request.RecipeCategoryIdId;
            await _repository.UpdateAsync(food, cancellationToken);
            return Unit.Value;
        }
    }
}