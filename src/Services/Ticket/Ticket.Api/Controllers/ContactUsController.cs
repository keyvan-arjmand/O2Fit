using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Common.Models;
using Ticket.Application.Dtos;
using Ticket.Application.UserContacts.V1.Commands.InsertContact;
using Ticket.Application.UserContacts.V1.Commands.SoftDeleteContact;
using Ticket.Application.UserContacts.V1.Queries.GetAllContact;
using Ticket.Application.UserContacts.V1.Queries.GetByIdContact;

namespace Ticket.Api.Controllers;

[ApiVersion("1")]
public class ContactUsController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;


    public ContactUsController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet("get-all-user-message")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ActionResult<PaginationResult<ContactDto>>> GetAllContacts(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllContactQuery(page, pageSize), cancellationToken);
    }

    [HttpGet("get-by-id-user-message")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ActionResult<ApiResult<ContactDto>>> GetByIdContact(string id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdContactQuery(id), cancellationToken);
        return new ApiResult<ContactDto>(result, "عملیات با موفقیت انجام شد",
            ApiResultStatusCode.Success, true);
    }

    [HttpPost("insert-user-message")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ApiResult> InsertContact(InsertContactCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }

    [HttpDelete("soft-delete-user-message")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ApiResult> SoftDeleteContact(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteContactCommand(id), cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
}