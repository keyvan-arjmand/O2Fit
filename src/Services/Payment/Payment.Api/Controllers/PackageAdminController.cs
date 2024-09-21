using MediatR;
using Payment.Application.PackageDietNutritionists.V1.Commands.InsertDietNutritionist;
using Payment.Application.PackageDietNutritionists.V1.Commands.SoftDeleteDietNutritionist;

namespace Payment.Api.Controllers
{
    [ApiVersion("1")]
    public class PackageAdminController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public PackageAdminController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        // [HttpGet("currency")]
        // [HasPermission(PermissionsConstants.CreatePermission)]
        // public IActionResult CurrencyValue()
        // {
        //     return Ok(EnumExtensions.GetEnumNameValues<Currency>());
        // }
        [HasPermission(PermissionsConstants.GetAllPackageAdmin)]
        [HttpGet]
        // public async Task<ApiResult<List<PackageDto>>> GetAllPackage()
        // {
        //     var result = await _mediator.Send(new GetAllPackageAdminQuery());
        //     return new ApiResult<List<PackageDto>>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        // }
        // [HasPermission(PermissionsConstants.GetAllPackagePaginationAdmin)]
        // [HttpGet("get-all-pagination")]
        // public async Task<ApiResult<PaginationResult<PackageDto>>> GetAllPackagePagination(int page, int pageSize)
        // {
        //     var result = await _mediator.Send(new GetAllPackageAdminPaginationQuery { Page = page, PageSize = pageSize });
        //     return new ApiResult<PaginationResult<PackageDto>>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        // }
        // [HasPermission(PermissionsConstants.GetByIdPackageAdmin)]
        // [HttpGet("get-by-id")]
        // public async Task<ApiResult<PackageDto>> GetById(string id, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(new GetPackageAdminByIdQuery { Id = id });
        //
        //     return new ApiResult<PackageDto>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        // }
        [HasPermission(PermissionsConstants.PostPackageAdmin)]
        [HttpPost]
        public async Task<ApiResult<string>> Post(InsertDietNutritionistCommand request, CancellationToken cancellationToken)
        {
            var package = await _mediator.Send(request, cancellationToken);
            return new ApiResult<string>(package, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        }

        // [HasPermission(PermissionsConstants.PutPackageAdmin)]
        // [HttpPut]
        // public async Task<ApiResult> Put(UpdatePackageAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        [HasPermission(PermissionsConstants.PatchTranslationNamePackageAdmin)]
        [HttpPatch("translation-name")]
        // public async Task<ApiResult> PatchTranslationName(UpdateTranslationNameAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HasPermission(PermissionsConstants.PatchTranslationDescriptionPackageAdmin)]
        // [HttpPatch("translation-description")]
        // public async Task<ApiResult> PatchTranslationDescription(UpdateTranslationDescriptionAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        [HasPermission(PermissionsConstants.DeletePackageAdmin)]
        [HttpDelete]
        public async Task<ApiResult> Delete(string id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SoftDeleteDietNutritionistCommand(id));

            return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        }
    }
}
