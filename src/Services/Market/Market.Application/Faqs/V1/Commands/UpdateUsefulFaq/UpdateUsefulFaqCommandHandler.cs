using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Domain.Aggregates.FAQAggregate;
using Market.Domain.Enums;

namespace Market.Application.Faqs.V1.Commands.UpdateUsefulFaq;

public class UpdateUsefulFaqCommandHandler : IRequestHandler<UpdateUsefulFaqCommand>
{
    private readonly IUnitOfWork _work;

    public UpdateUsefulFaqCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(UpdateUsefulFaqCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<FrequentlyAskedQuestion>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"FAQ not found {request.Id}");
        if (request.UseFulStatus == UseFulStatus.Useful)
        {
            result.Useful++;
        }
        else
        {
            result.UnUseful++;
        }

        await _work.GenericRepository<FrequentlyAskedQuestion>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<FrequentlyAskedQuestion, object>>[]
                {
                    x => x.Useful,
                    x => x.UnUseful,
                }, null, cancellationToken);
    }
}