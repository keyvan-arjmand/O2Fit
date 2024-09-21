using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Faqs.V1.Commands.UpdateUsefulFaq;
using Market.Domain.Aggregates.AppLearnAggregate;
using Market.Domain.Aggregates.FAQAggregate;
using Market.Domain.Enums;

namespace Market.Application.AppLearns.V1.Commands.UpdateUsefulAppLearn;

public class UpdateUsefulAppLearnCommandHandler : IRequestHandler<UpdateUsefulFaqCommand>
{
    private readonly IUnitOfWork _work;

    public UpdateUsefulAppLearnCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(UpdateUsefulFaqCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppLearn>()
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

        await _work.GenericRepository<AppLearn>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<AppLearn, object>>[]
                {
                    x => x.Useful,
                    x => x.UnUseful,
                }, null, cancellationToken);
    }
}