using Identity.V2.Application.Countries.V1.Commands;
using Identity.V2.Application.Countries.V1.Commands.AddCityToState;
using Identity.V2.Application.Countries.V1.Commands.AddStateToCountry;
using Identity.V2.Application.Countries.V1.Commands.EditCityInState;
using Identity.V2.Application.Countries.V1.Commands.EditStateInCountry;
using Identity.V2.Application.Countries.V1.Commands.SoftDeleteByOldSystemId;
using Identity.V2.Application.Countries.V1.Commands.UpdateUnCountryInfo;
using Identity.V2.Application.Countries.V1.Queries.GetAllCountries;
using Identity.V2.Application.Dtos.Countries;

namespace Identity.V2.Api.Controllers
{
    [ApiVersion("1")]
    public class CountryController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HasPermission(PermissionsConstants.GetAllCountries)]
        [HttpGet("get-all-countries")]
        public async Task<ActionResult> GetAllCountries(CancellationToken cancellationToken)
        {
            var countries = await _mediator.Send(new GetAllCountriesQuery(), cancellationToken);
            return Ok(new ApiResult<List<CountryDto>>(countries, string.Empty, ApiResultStatusCode.Success));
        }

        [HasPermission(PermissionsConstants.AddStateToCountry)]
        [HttpPost("add-state-to-country")]
        public async Task<ActionResult> AddStateToCountry([FromBody] AddStateToCountryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }

        [HasPermission(PermissionsConstants.AddCityToState)]
        [HttpPost("add-city-to-state")]
        public async Task<ActionResult> AddCityToState([FromBody] AddCityToStateCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }
        
        
        [HasPermission(PermissionsConstants.UpdateUnCountryInfo)]
        [HttpPatch("update-un-country-info")]
        public async Task<ActionResult> UpdateUnCountryInfo([FromBody] UpdateUnCountryInfoCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }

        [HasPermission(PermissionsConstants.UpdateNameOfState)]
        [HttpPatch("update-name-of-state")]
        public async Task<ActionResult> UpdateNameOfState([FromBody] EditStateInCountryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }
        
        [HasPermission(PermissionsConstants.UpdateNameOfCity)]
        [HttpPatch("update-name-of-city")]
        public async Task<ActionResult> UpdateNameOfCity([FromBody] EditCityInStateCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }

        //[AllowAnonymous]
        //[HttpDelete("soft-delete-country-by-old-system-id")]
        //public async Task<ActionResult> SoftDeleteCountryByOldSystemId([FromQuery] SoftDeleteByOldSystemIdCommand command,
        //    CancellationToken cancellationToken)
        //{
        //    await _mediator.Send(command,cancellationToken);
        //    return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        //}
    }
}