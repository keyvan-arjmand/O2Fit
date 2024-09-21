using Market.Application.AppLearnCategories.V1.Commands.SoftDeleteAppLearnCategory;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Domain.Aggregates.AppLearnCategoryAggregate;
using Market.Domain.Aggregates.AppLearnSubCategoryAggregate;

namespace Market.Application.AppLearnSubCategories.V1.Commands.SoftDeleteAppLearnSubCategory;

public class SoftDeleteAppLearnSubCategoryCommandHandler : IRequestHandler<SoftDeleteAppLearnSubCategoryCommand>
{
    private readonly IUnitOfWork _work;
    

    public SoftDeleteAppLearnSubCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteAppLearnSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppLearnSubCategory>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("AppLearnCategory Not Found");
        result.IsDelete = false;
        await _work.GenericRepository<AppLearnSubCategory>()
            .SoftDeleteByIdAsync(result.Id, result, null, cancellationToken);
    }
}