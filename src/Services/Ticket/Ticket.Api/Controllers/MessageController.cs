using MediatR;
using Ticket.Application.Dtos;
using Ticket.Application.Messages.V1.Commands.InsertUserMessage;
using Ticket.Application.Messages.V1.Commands.SoftDeleteUserMessage;
using Ticket.Application.Messages.V1.Queries.GetAllUserMessage;
using Ticket.Application.Messages.V1.Queries.GetByIdUserMessage;

namespace Ticket.Api.Controllers;

[ApiVersion("1")]
public class MessageController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public MessageController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet("get-all-user-message")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ActionResult<ApiResult<List<MessageDto>>>> GetAllUserMessage(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllUserMessageQuery(_currentUserService.UserId!), cancellationToken);
        return new ApiResult<List<MessageDto>>(result, "عملیات با موفقیت انجام شد",
            ApiResultStatusCode.Success, true);
    }
    [HttpGet("get-by-id-user-message")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ActionResult<ApiResult<MessageDto>>> GetByIdUserMessage(string id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdUserMessageQuery(id), cancellationToken);
        return new ApiResult<MessageDto>(result, "عملیات با موفقیت انجام شد",
            ApiResultStatusCode.Success, true);
    }
    [HttpPost("insert-user-message")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ApiResult> InsertUserMessage(InsertUserMessageCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
    [HttpDelete("soft-delete-user-message")]
    [HasPermission(PermissionsConstants.GetAllPackagePaginationNutritionist)]
    public async Task<ApiResult> SoftDeleteUserMessage(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteUserMessageCommand(id), cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
}