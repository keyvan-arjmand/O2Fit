
namespace Identity.V2.Application.PermissionCategories.V1.Queries.GetAllCategoryPermissionsWithPermissionsTreeView;

public class GetAllCategoryPermissionsWithPermissionsQueryHandler : IRequestHandler<
    GetAllCategoryPermissionsWithPermissionsQuery, List<CategoryPermissionWithPermissionsForTreeViewDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllCategoryPermissionsWithPermissionsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<CategoryPermissionWithPermissionsForTreeViewDto>> Handle(GetAllCategoryPermissionsWithPermissionsQuery request, CancellationToken cancellationToken)
    {
        var data=  await _uow.GenericRepository<CategoryPermission>().GetAllAsync(cancellationToken).ConfigureAwait(false);
        return data.ToDto<CategoryPermissionWithPermissionsForTreeViewDto>().ToList();
    }
}