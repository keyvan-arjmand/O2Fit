using Currency.Application.Currencies.V1.Commands.CreateCurrency;
using Currency.Application.Currencies.V1.Commands.DeleteCurrency;
using Currency.Application.Currencies.V1.Commands.UpdateCurrency;
using Currency.Application.Currencies.V1.Query.GetAllCurrency;
using Currency.Application.Currencies.V1.Query.GetByIdCurrency;
using Currency.Application.Currencies.V1.Query.GetByNameCurrency;
using Currency.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Currency.Api.Controllers
{
    [ApiVersion("1")]
    public class CurrencyController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CurrencyController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("get-all-currency")]
        [HasPermission(PermissionsConstants.GetAllCurrency)]
        public async Task<ApiResult<List<CurrencyDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllCurrencyQuery());
            return new ApiResult<List<CurrencyDto>>(result, string.Empty, ApiResultStatusCode.Success, true);
        }

        [HttpGet("get-by-id")]
        [HasPermission(PermissionsConstants.GetByIdCurrency)]
        public async Task<ApiResult<CurrencyDto>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdCurrencyQuery(id), cancellationToken);

            return new ApiResult<CurrencyDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        }

        [HttpGet("get-by-name")]
        [HasPermission(PermissionsConstants.GetByNameCurrency)]
        public async Task<ApiResult<CurrencyDto>> GetByName(string name, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCurrencyByCodeQuery(name), cancellationToken);

            return new ApiResult<CurrencyDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        }

        [HttpPost("insert-currency")]
        [HasPermission(PermissionsConstants.PostCurrency)]
        public async Task<ApiResult> Post(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        }

        [HttpPatch("partial-update-currency")]
        [HasPermission(PermissionsConstants.PutCurrency)]
        public async Task<ApiResult> Put(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        }

        [HttpDelete("soft-delete-currency")]
        [HasPermission(PermissionsConstants.DeleteCurrency)]
        public async Task<ApiResult> Delete(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
        }
    }
}