using Discount.Application.DiscountPackageO2Fit.V1.Query.GetAllDiscountPackageO2Fit;
using Discount.Application.DiscountPackagesNutritionist.V1.Commands.CreateDiscountPackageNutritionist;
using Discount.Application.DiscountPackagesNutritionist.V1.Commands.DeleteDiscountPackageNutritionist;
using Discount.Application.DiscountPackagesNutritionist.V1.Commands.UpdateDiscountPackageNutritionist;
using Discount.Application.DiscountPackagesNutritionist.V1.Query.GetAllDiscountPackageNutritionist;
using Discount.Application.DiscountPackagesNutritionist.V1.Query.GetByIdDiscountPackageNutritionist;
using Discount.Application.Dtos;
using MediatR;

namespace Discount.Api.Controllers
{
    [ApiVersion("1")]
    public class DiscountPackageController : BaseApiController
    {
        private readonly IMediator _mediator;
        public DiscountPackageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // [HttpGet("get-all-discount-package-nutritionist")]
        // [HasPermission(PermissionsConstants.GetAllDiscountPackageNutritionist)]
        // public async Task<ApiResult<List<DiscountPackageDto>>> GetPackageNutritionist()
        // {
        //     var result = await _mediator.Send(new GetAllDiscountPackageNutritionistQuery());
        //     return new ApiResult<List<DiscountPackageDto>>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpGet("get-by-id-discount-package-nutritionist")]
        // [HasPermission(PermissionsConstants.GetByIdDiscountPackageNutritionist)]
        // public async Task<ApiResult<DiscountPackageDto>> GetByIdPackageNutritionist(string id, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(new GetByIdDiscountNutritionistPackageQuery(id), cancellationToken);
        //     return new ApiResult<DiscountPackageDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpGet("get-all-discount-package-admin")]
        // [HasPermission(PermissionsConstants.GetAllDiscountPackageAdmin)]
        // public async Task<ApiResult<List<DiscountPackageDto>>> GetPackageAdmin()
        // {
        //     var result = await _mediator.Send(new GetAllDiscountPackageAdminQuery());
        //     return new ApiResult<List<DiscountPackageDto>>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpGet("get-by-id-discount-package-admin")]
        // [HasPermission(PermissionsConstants.GetByIdDiscountPackageAdmin)]
        // public async Task<ApiResult<DiscountPackageDto>> GetByIdPackageAdmin(string id, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(new GetByIdDiscountPackageAdminQuery(id), cancellationToken);
        //     return new ApiResult<DiscountPackageDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpPost("insert-discount-package-admin")]
        // [HasPermission(PermissionsConstants.PostDiscountPackageAdmin)]
        // public async Task<ApiResult> PostPackageAdmin(CreateDiscountPackageAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpPost("insert-discount-package-nutritionist")]
        // [HasPermission(PermissionsConstants.PostDiscountPackageNutritionist)]
        // public async Task<ApiResult> PostPackageNutritionist(CreateDiscountPackageNutritionistCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpPatch("partial-update-discount-package-admin")]
        // [HasPermission(PermissionsConstants.PatchDiscountPackageAdmin)]
        // public async Task<ApiResult> PatchPackageAdmin(UpdateDiscountPackageAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpPatch("partial-update-discount-package-nutritionist")]
        // [HasPermission(PermissionsConstants.PatchDiscountPackageNutritionist)]
        // public async Task<ApiResult> PatchPackageNutritionist(UpdateDiscountPackageNutritionistCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpDelete("soft-delete-discount-package-nutritionist")]
        // [HasPermission(PermissionsConstants.DeleteDiscountPackageNutritionist)]
        // public async Task<ApiResult> DeletePackageNutritionist(DeleteDiscountPackageNutritionistCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpDelete("partial-update-delete-discount-package-admin")]
        // [HasPermission(PermissionsConstants.DeleteDiscountPackageAdmin)]
        // public async Task<ApiResult> DeletePackageAdmin(DeleteDiscountPackageAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
    }
}
