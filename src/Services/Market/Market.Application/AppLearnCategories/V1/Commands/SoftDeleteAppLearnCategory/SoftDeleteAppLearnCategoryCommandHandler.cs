using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Domain.Aggregates.AppLearnCategoryAggregate;

namespace Market.Application.AppLearnCategories.V1.Commands.SoftDeleteAppLearnCategory;

public class SoftDeleteAppLearnCategoryCommandHandler : IRequestHandler<SoftDeleteAppLearnCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteAppLearnCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteAppLearnCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppLearnCategory>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("AppLearnCategory Not Found");
        result.IsDelete = false;
        await _work.GenericRepository<AppLearnCategory>()
            .SoftDeleteByIdAsync(result.Id, result, null, cancellationToken);
    }
}