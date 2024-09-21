using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Command.Tips
{
    public class SoftDeleteTipCommandHandler: IRequestHandler<SoftDeleteTipCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Tip> _repository;

        public SoftDeleteTipCommandHandler(IRepository<Tip> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteTipCommand request, CancellationToken cancellationToken)
        {
            var tip = await _repository.Table.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (tip == null)
                throw new AppException(ApiResultStatusCode.ServerError);

            tip.IsDelete = true;

            await _repository.UpdateAsync(tip, cancellationToken);

            return Unit.Value;
        }
    }
}