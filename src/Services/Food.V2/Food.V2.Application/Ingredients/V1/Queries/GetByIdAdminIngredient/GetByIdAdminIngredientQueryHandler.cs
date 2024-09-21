namespace Food.V2.Application.Ingredients.V1.Queries.GetByIdAdminIngredient;

public class GetByIdAdminIngredientQueryHandler : IRequestHandler<GetByIdAdminIngredientQuery,GetByIdAdminIngredientDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public GetByIdAdminIngredientQueryHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task<GetByIdAdminIngredientDto> Handle(GetByIdAdminIngredientQuery request, CancellationToken cancellationToken)
    {
        var ingredient = await _uow.GenericRepository<Ingredient>().GetByIdAsync(request.Id, cancellationToken);
        if (ingredient == null)
            throw new NotFoundException(nameof(Ingredient), request.Id);

        var result = ingredient.ToDto<GetByIdAdminIngredientDto>();

        if (!string.IsNullOrEmpty(result.ThumbName))
        {
            result.ThumbName =
                await _fileService.GetImageAsBase64Async(PathAddressesConstants.IngredientImages + "/" +
                                                         result.ThumbName);
        }
        result.NutrientValue = string.Join(",", ingredient.NutrientValue.Select(s => s.Value.ToString()));
        return result;
    }
}