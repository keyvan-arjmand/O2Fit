using MediatR;
using Payment.Application.Common.Models;
using Payment.Application.Dtos;

namespace Payment.Api.Controllers
{
    [ApiVersion("1")]
    public class PackageNutritionistController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
       private readonly ICurrentUserService _currentUserService;
        public PackageNutritionistController(IMapper mapper, IMediator mediator, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _currentUserService = currentUserService;
            _currentUserService = currentUserService;
        }

        // [HttpGet("get-all-pagination")]
        // [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
        // public async Task<ApiResult<PaginationResult<PackageDto>>> GetAllPackagePagination(int page, int pageSize)
        // {
        //     var result = await _mediator.Send(new GetAllPackageNutritionistPaginationQuery { Page = page, PageSize = pageSize });
        //     return new ApiResult<PaginationResult<PackageDto>>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        // }
        // [HttpGet("currency")]
        //
        // public IActionResult CurrencyValue()
        // {
        //     return Ok(EnumExtensions.GetEnumNameValues<Currency>());
        // }


        // [HttpGet]
        // [HasPermission(PermissionsConstants.GetAllPackageNutritionist)]
        // public async Task<ApiResult<List<PackageDto>>> Get()
        // {
        //     var result = await _mediator.Send(new GetPackageNutritionistQuery());
        //     return new ApiResult<List<PackageDto>>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        // }

        //[HttpGet("Get-All-PackageAdmin")]
        //public async Task<ApiResult<List<PackageDto>>> GetAllPackageAdmin()
        //{
        //    var result = await _mediator.Send(new GetAllPackageNutritionistQuery());
        //    return new ApiResult<List<PackageDto>>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        //}

        // [HttpGet("get-by-id")]
        // [HasPermission(PermissionsConstants.GetByIdPackageNutritionist)]
        // public async Task<ApiResult<PackageDto>> GetById(string id, CancellationToken cancellationToken)
        // {
        //     var result = await _mediator.Send(new GetPackageNutritionistByIdQuery { Id = id },cancellationToken);
        //
        //     return new ApiResult<PackageDto>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpPost]
        // [HasPermission(PermissionsConstants.PostPackageNutritionist)]
        // public async Task<ApiResult<string>> Post(CreatePackageNutritionistCommand request, CancellationToken cancellationToken)
        // {
        //     var package = await _mediator.Send(request, cancellationToken);
        //     return new ApiResult<string>(package, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
        // }
        //
        // [HasPermission(PermissionsConstants.PutPackageNutritionist)]
        // [HttpPut]
        // public async Task<ApiResult> Put(UpdatePackageNutritionistCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HasPermission(PermissionsConstants.PatchTranslationNamePackageNutritionist)]
        // [HttpPatch("translation-name")]
        // public async Task<ApiResult> PatchTranslationName(UpdateTranslationNameNutritionistCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        // [HttpPatch("translation-description")]
        // [HasPermission(PermissionsConstants.PatchTranslationDescriptionPackageNutritionist)]
        // public async Task<ApiResult> PatchTranslationDescription(UpdateTranslationDescriptionNutritionistCommand request, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(request, cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }
        //
        // [HttpDelete]
        // [HasPermission(PermissionsConstants.DeletePackageNutritionist)]
        // public async Task<ApiResult> Delete(string id, CancellationToken cancellationToken)
        // {
        //     await _mediator.Send(new DeletePackageNutritionistCommand(id),cancellationToken);
        //     return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        // }

    }
}
