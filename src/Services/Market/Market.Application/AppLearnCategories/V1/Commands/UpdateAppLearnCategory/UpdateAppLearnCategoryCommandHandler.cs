using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Domain.Aggregates.AppLearnCategoryAggregate;

namespace Market.Application.AppLearnCategories.V1.Commands.UpdateAppLearnCategory;

public class UpdateAppLearnCategoryCommandHandler : IRequestHandler<UpdateAppLearnCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public UpdateAppLearnCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(UpdateAppLearnCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppLearnCategory>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"Cat not found {request.Id}");
        
        result.Title.English = request.Title.English;
        result.Title.Arabic = request.Title.Arabic;
        result.Title.Persian = request.Title.Persian;
        
        await _work.GenericRepository<AppLearnCategory>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<AppLearnCategory, object>>[]
                {
                    x => x.Title.Persian,
                    x => x.Title.English,
                    x => x.Title.Arabic,
                }, null, cancellationToken);
    }
}