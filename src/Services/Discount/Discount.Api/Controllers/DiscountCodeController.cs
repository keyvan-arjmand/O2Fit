using Discount.Application.Dtos;
using MediatR;

namespace Discount.Api.Controllers
{
    [ApiVersion("1")]
    public class DiscountCodeController : BaseApiController
    {
        private readonly IMediator _mediator;
        public DiscountCodeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //Nutritionist
        // [HttpGet("get-all-discount-code-nutritionist")]
        // [HasPermission(PermissionsConstants.GetAllNutritionist)]
        // public async Task<ApiResult<List<DiscountDto>>> GetNutritionist()
        // {
        //     var result = await _mediator.Send(new GetAllDiscountNutritionistQuery());
        //     return new ApiResult<List<DiscountDto>>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpGet("get-by-id-discount-code-nutritionist")]
        // [HasPermission(PermissionsConstants.GetByIdNutritionist)]
        // public async Task<ApiResult<DiscountDto>> GetByIdNutritionist(string id, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(new GetByIdDiscountNutritionistQuery(id), cancellationToken);
        //     return new ApiResult<DiscountDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpGet("get-by-code-discount-code-nutritionist")]
        // [HasPermission(PermissionsConstants.GetByCodeNutritionist)]
        // public async Task<ApiResult<DiscountDto>> GetByCodeNutritionist(string code, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(new GetByCodeDiscountNutritionistQuery(code), cancellationToken);
        //     return new ApiResult<DiscountDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        // //Admin
        // [HttpGet("get-all-discount-code-admin")]
        // [HasPermission(PermissionsConstants.GetAllDiscountAdmin)]
        // public async Task<ApiResult<List<DiscountDto>>> GetAdmin()
        // {
        //     var result = await _mediator.Send(new GetAllDiscountAdminQuery());
        //     return new ApiResult<List<DiscountDto>>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpGet("get-by-id-discount-code-admin")]
        // [HasPermission(PermissionsConstants.GetByIdDiscountAdmin)]
        // public async Task<ApiResult<DiscountDto>> GetByIdAdmin(string id, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(new GetByIdDiscountAdminQuery(id), cancellationToken);
        //     return new ApiResult<DiscountDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpGet("get-by-code-discount-code-admin")]
        // [HasPermission(PermissionsConstants.GetByCodeDiscountAdmin)]
        // public async Task<ApiResult<DiscountDto>> GetByCodeAdmin(string code, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(new GetByCodeDiscountAdminQuery(code), cancellationToken);
        //     return new ApiResult<DiscountDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpPost("discount-code-discount-code-admin")]
        // [HasPermission(PermissionsConstants.PostDiscountAdmin)]
        // public async Task<ApiResult> PostAdmin(CreateDiscountAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpPost("generator-code-discount-code-admin")]
        // [HasPermission(PermissionsConstants.PostWithGeneratorDiscountAdmin)]
        // public async Task<ApiResult> PostWithGeneratorAdmin(CreateDiscountWithGenerationAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpPost("apply-discount-code-code")]
        // [HasPermission(PermissionsConstants.ApplyDiscountCode)]
        // public async Task<ApiResult<ApplyDiscountCodeDto>> ApplyDiscountCode(ApplyDiscountCodeAdminCommand request, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(request, cancellationToken);
        //     return new ApiResult<ApplyDiscountCodeDto>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        // }
        // [HttpPatch("update-discount-code-admin")]
        // [HasPermission(PermissionsConstants.PatchDiscountDiscountAdmin)]
        // public async Task<ApiResult> PatchDiscountAdmin(UpdateDiscountAdminCommand request, CancellationToken cancellationToken)
        // {
        //
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpPatch("active-discount-code-admin")]
        // [HasPermission(PermissionsConstants.ActiveDiscountAdmin)]
        // public async Task<ApiResult> ActiveDiscountCodeAdmin(ActivateDiscountAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpPatch("update-translate-discount-code-admin")]
        // [HasPermission(PermissionsConstants.PatchDiscountTranslateAdmin)]
        // public async Task<ApiResult> UpdateTranslateAdmin(UpdateTranslateAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpDelete("delete-discount-code-admin")]
        // [HasPermission(PermissionsConstants.DeleteDiscountAdmin)]
        // public async Task<ApiResult> DeleteAdmin(DeleteDiscountAdminCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
    }
}
