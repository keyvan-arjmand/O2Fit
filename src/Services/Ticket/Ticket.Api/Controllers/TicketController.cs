using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Dtos;
using Ticket.Application.Tickets.V1.Commands;
using Ticket.Application.Tickets.V1.Commands.InsertUserTicket;
using Ticket.Application.Tickets.V1.Commands.PartialUpdateUserTicket;
using Ticket.Application.Tickets.V1.Queries.GetTicketById;

namespace Ticket.Api.Controllers;

[ApiVersion("1")]
public class TicketController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public TicketController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }
   
    [HasPermission(PermissionsConstants.PostUserTrackSpecification)]
    [HttpGet("get-track-water-by-date")]
    public async Task<ActionResult<UserTicketDto>> GetTicketById(string id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTicketByIdQuery(id), cancellationToken);
        return Ok(new ApiResult<UserTicketDto>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [HttpPost("insert-user-ticket")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ApiResult> InsertUserTicket(InsertUserTicketCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }

    [HttpPatch("partial-update-user-ticket")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ApiResult> PartialUpdateUserTicket(PartialUpdateUserTicketCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
}