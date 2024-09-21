using Food.V2.Application.Brands.V1.Commands.InsertBrand;
using Food.V2.Application.Brands.V1.Queries.SearchByName;
using Food.V2.Application.Dtos.Brand;
using Microsoft.AspNetCore.Mvc;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class BrandController : BaseApiController
{
    private readonly IMediator _mediator;

    public BrandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("search-brand-by-name")]
    public async Task<IActionResult> SearchBrandByName(string name, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SearchBrandByNameQuery
        {
            Language = string.IsNullOrEmpty(Request.Headers.FirstOrDefault(x => x.Key.Equals("Language")).Value)
                ? "Persian"
                : Request.Headers.FirstOrDefault(x => x.Key.Equals("Language")).Value,
            Name = name
        }, cancellationToken);
        return Ok(new ApiResult<List<BrandDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPost("insert-brand")]
    public async Task<ApiResult> InsertBrand(InsertBrandCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
}