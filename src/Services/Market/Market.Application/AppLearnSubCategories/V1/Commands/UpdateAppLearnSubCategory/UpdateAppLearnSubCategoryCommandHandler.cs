using Market.Application.AppLearnCategories.V1.Commands.UpdateAppLearnCategory;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Domain.Aggregates.AppLearnCategoryAggregate;
using Market.Domain.Aggregates.AppLearnSubCategoryAggregate;

namespace Market.Application.AppLearnSubCategories.V1.Commands.UpdateAppLearnSubCategory;

public class UpdateAppLearnSubCategoryCommandHandler : IRequestHandler<UpdateAppLearnSubCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public UpdateAppLearnSubCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(UpdateAppLearnSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var cat = await _work.GenericRepository<AppLearnCategory>().GetByIdAsync(request.CategoryId, cancellationToken);
        if (cat == null) throw new NotFoundException($"AppLearnCategory not found {request.CategoryId}");
        var result = await _work.GenericRepository<AppLearnSubCategory>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"Cat not found {request.Id}");
        
        result.Title.English = request.Title.English;
        result.Title.Arabic = request.Title.Arabic;
        result.Title.Persian = request.Title.Persian;
        result.CategoryId = cat.Id.StringToObjectId();
        result.CategoryName.English = cat.Title.English;
        result.CategoryName.Arabic = cat.Title.Arabic;
        result.CategoryName.Persian = cat.Title.Persian;
        await _work.GenericRepository<AppLearnSubCategory>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<AppLearnSubCategory, object>>[]
                {
                    x => x.Title.Persian,
                    x => x.Title.English,
                    x => x.Title.Arabic,
                    x => x.CategoryName.Persian,
                    x => x.CategoryName.English,
                    x => x.CategoryName.Arabic,
                    x => x.CategoryId,
                }, null, cancellationToken);
    }
}