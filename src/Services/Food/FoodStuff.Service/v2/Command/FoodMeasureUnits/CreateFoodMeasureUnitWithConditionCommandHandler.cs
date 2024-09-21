using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v2.Command.FoodMeasureUnits
{
    public class CreateFoodMeasureUnitWithConditionCommandHandler: IRequestHandler<CreateFoodMeasureUnitWithConditionCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<FoodMeasureUnit> _repository;

        public CreateFoodMeasureUnitWithConditionCommandHandler(IRepository<FoodMeasureUnit> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateFoodMeasureUnitWithConditionCommand request, CancellationToken cancellationToken)
        {
            if (request.MeasureUnitId != 36 && request.MeasureUnitId != 37 && request.MeasureUnitId != 58)
            {
                var foodMeasureUnit = new FoodMeasureUnit
                {
                    FoodId = request.FoodId,
                    MeasureUnitId = request.MeasureUnitId
                };
                await _repository.AddAsync(foodMeasureUnit, cancellationToken);
            }

            return Unit.Value;
        }
    }
}