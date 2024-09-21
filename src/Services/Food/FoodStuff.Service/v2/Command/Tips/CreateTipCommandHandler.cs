using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v2.Command.Tips
{
    public class CreateTipCommandHandler: IRequestHandler<CreateTipCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Tip> _repository;

        public CreateTipCommandHandler(IRepository<Tip> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tip = new Tip
                {
                    DateInsert = DateTime.Now,
                    DescriptionId = request.DescriptionId,
                    RecipeId = request.RecipeId
                };
                await _repository.AddAsync(tip, cancellationToken);
                return Unit.Value;

            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError);
            }
        }
    }
}