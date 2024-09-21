using Market.Application.AppLearnCategories.V1.Commands.InsertAppLearnCategory;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.AppLearnCategoryAggregate;
using Market.Domain.Aggregates.AppLearnSubCategoryAggregate;

namespace Market.Application.AppLearnSubCategories.V1.Commands.InsertAppLearnSubCategory;

public class InsertAppLearnSubCategoryCommandHandler:IRequestHandler<InsertAppLearnSubCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public InsertAppLearnSubCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }


    public async Task Handle(InsertAppLearnSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppLearnCategory>().GetByIdAsync(request.CategoryId, cancellationToken);
        if (result == null) throw new NotFoundException($"AppLearnCategory not found {request.CategoryId}");
        await _work.GenericRepository<AppLearnSubCategory>().InsertOneAsync(new AppLearnSubCategory
        {
            CategoryId = result.Id.StringToObjectId(),
            CategoryName = result.Title.MapTo<AppLearnSubCategoryTranslation, AppLearnCategoryTranslation>(),
            Title = request.Title.MapTo<AppLearnSubCategoryTranslation, TranslationDto>()
        }, cancellationToken: cancellationToken);
    }
}