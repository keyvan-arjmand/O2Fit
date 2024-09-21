using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Domain.Aggregates.FAQCategoryAggregate;

namespace Market.Application.FaqCategories.V1.Commands.UpdateFaqCategory;

public class UpdateFaqCategoryCommandHandler : IRequestHandler<UpdateFaqCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public UpdateFaqCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(UpdateFaqCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<FaqCategory>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"Category FAQ not found {request.Id}");
        result.Title.English = request.Title.English;
        result.Title.Arabic = request.Title.Arabic;
        result.Title.Persian = request.Title.Persian;
        await _work.GenericRepository<FaqCategory>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<FaqCategory, object>>[]
                {
                    x => x.Title.Persian,
                    x => x.Title.English,
                    x => x.Title.Arabic,
                }, null, cancellationToken);
    }
}