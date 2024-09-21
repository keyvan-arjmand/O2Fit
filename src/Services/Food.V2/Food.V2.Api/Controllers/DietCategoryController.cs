using Food.V2.Application.DietCategories.V1.Commands.CreateDietCategory;
using Food.V2.Application.DietCategories.V1.Commands.SofDeleteCategory;
using Food.V2.Application.DietCategories.V1.Commands.UpdateDietCategory;
using Food.V2.Application.DietCategories.V1.Queries.GetAllDietCategory;
using Food.V2.Application.DietCategories.V1.Queries.GetAllDietCategoryPagination;
using Food.V2.Application.DietCategories.V1.Queries.GetCategoryById;
using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.FoodCategories.V1.Queries.GetAllCategory;
using Microsoft.AspNetCore.Mvc;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class DietCategoryController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _environment;

    public DietCategoryController(IMediator mediator, IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _environment = environment;
    }

    [HttpGet("get-all-diet-category-pagination")]
    [HasPermission(PermissionsConstants.GetAllDietCategoryPagination)]
    public async Task<IActionResult> GetAllDietCategoryPagination(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllDietCategoryPaginationQuery(page, pageSize), cancellationToken);
        return Ok(new ApiResult<List<DietCategoryDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }
    [HttpGet("get-all-active")]
    [HasPermission(PermissionsConstants.GetAllActiveDietCategory)]
    public async Task<IActionResult> GetAllActiveDietCategory(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllDietCategoryQuery(), cancellationToken);
        return Ok(new ApiResult<List<DietCategoryDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }
    [HttpGet("get-by-id-diet-category")]
    [HasPermission(PermissionsConstants.GetByIdDietCategory)]
    public async Task<IActionResult> GetByIdDietCategory(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDietCategoryByIdQuery(id), cancellationToken);
        return Ok(new ApiResult<DietCategoryDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HttpPost("diet-category")]
    [HasPermission(PermissionsConstants.PostDietCategory)]
    public async Task<ApiResult> PostDietCategory(CreateDietCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ImageName))
        {
            var base64EncodedBytes =
                Convert.FromBase64String(request.ImageName.PadRight(
                    request.ImageName.Length + (request.ImageName.Length * 3) % 4,
                    '='));

            string _Path = Path.Combine(_environment.WebRootPath, "UserBodySize");

            if (!Directory.Exists(_Path))
            {
                Directory.CreateDirectory(_Path);
            }

            string _FileName = Guid.NewGuid() + ".jpg";
            var _Address = Path.Combine(_Path, _FileName);

            await System.IO.File.WriteAllBytesAsync(_Address, base64EncodedBytes, cancellationToken);
            request.ImageName = _FileName;
        }

        if (!string.IsNullOrWhiteSpace(request.BannerImageName))
        {
            var base64EncodedBytes =
                Convert.FromBase64String(request.BannerImageName.PadRight(
                    request.BannerImageName.Length + (request.BannerImageName.Length * 3) % 4,
                    '='));

            string _Path = Path.Combine(_environment.WebRootPath, "UserBodySize");

            if (!Directory.Exists(_Path))
            {
                Directory.CreateDirectory(_Path);
            }

            string _FileName = Guid.NewGuid() + ".jpg";
            var _Address = Path.Combine(_Path, _FileName);

            await System.IO.File.WriteAllBytesAsync(_Address, base64EncodedBytes, cancellationToken);
            request.BannerImageName = _FileName;
        }

        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }

    [HttpPatch("diet-category")]
    [HasPermission(PermissionsConstants.PatchDietCategory)]
    public async Task<ApiResult> PatchDietCategory(UpdateDietCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ImageName))
        {
            var base64EncodedBytes =
                Convert.FromBase64String(request.ImageName.PadRight(
                    request.ImageName.Length + (request.ImageName.Length * 3) % 4,
                    '='));

            string _Path = Path.Combine(_environment.WebRootPath, "UserBodySize");

            if (!Directory.Exists(_Path))
            {
                Directory.CreateDirectory(_Path);
            }

            string _FileName = Guid.NewGuid() + ".jpg";
            var _Address = Path.Combine(_Path, _FileName);

            await System.IO.File.WriteAllBytesAsync(_Address, base64EncodedBytes, cancellationToken);
            request.ImageName = _FileName;
        }

        if (!string.IsNullOrWhiteSpace(request.BannerImageName))
        {
            var base64EncodedBytes =
                Convert.FromBase64String(request.BannerImageName.PadRight(
                    request.BannerImageName.Length + (request.BannerImageName.Length * 3) % 4,
                    '='));

            string _Path = Path.Combine(_environment.WebRootPath, "UserBodySize");

            if (!Directory.Exists(_Path))
            {
                Directory.CreateDirectory(_Path);
            }

            string _FileName = Guid.NewGuid() + ".jpg";
            var _Address = Path.Combine(_Path, _FileName);

            await System.IO.File.WriteAllBytesAsync(_Address, base64EncodedBytes, cancellationToken);
            request.BannerImageName = _FileName;
        }

        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
    [HttpDelete("soft-delete-diet-category")]
    [HasPermission(PermissionsConstants.SoftDeleteDietCategory)]
    public async Task<ApiResult> SoftDeleteDietCategory(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SofDeleteDietCategoryCommand(id), cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
}