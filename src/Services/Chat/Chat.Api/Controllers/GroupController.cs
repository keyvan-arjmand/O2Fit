using Chat.Application.ChatFiles.V1.Commands.DeleteChatFileByUrl;
using Chat.Application.Common.Constants;
using Chat.Application.Common.Models;
using Chat.Application.Dtos.Groups;
using Chat.Application.Dtos.Messages;
using Chat.Application.Groups.V1.Commands.ReportGroup;
using Chat.Application.Groups.V1.Queries.GetAllPaginated;
using Chat.Application.Groups.V1.Queries.GetGroupById;
using Chat.Application.Groups.V1.Queries.GetGroupByName;
using Chat.Application.Groups.V1.Queries.GetReportedGroupsPaginated;

namespace Chat.Api.Controllers;

[ApiVersion("1")]
public class GroupController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IHubContext<ChatHub> _chatHub;
    private readonly IFileService _fileService;

    public GroupController(IMediator mediator, IHubContext<ChatHub> chatHub, ICurrentUserService currentUserService,
        IFileService fileService)
    {
        _mediator = mediator;
        _chatHub = chatHub;
        _currentUserService = currentUserService;
        _fileService = fileService;
    }

    [HasPermission(PermissionsConstants.GetGroupById)]
    [HttpGet("get-group-by-id")]
    public async Task<ActionResult> GetGroupById([FromQuery] string id, CancellationToken cancellationToken)
    {
        var group = await _mediator.Send(new GetGroupByIdQuery(id), cancellationToken);
        return Ok(new ApiResult<GetGroupByIdDto>(group, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.GetReportedGroupsPaginated)]
    [HttpGet("get-reported-groups-paginated")]
    public async Task<ActionResult> GetReportedGroupsPaginated([FromQuery] GetReportedGroupsPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var group = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<PaginationResult<GetGroupPaginatedDto>>(group, string.Empty,
            ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.GetAllGroupsPaginated)]
    [HttpGet("get-all-groups-paginated")]
    public async Task<ActionResult> GetAllGroupsPaginated([FromQuery] GetAllPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var group = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<PaginationResult<GetGroupPaginatedDto>>(group, string.Empty,
            ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.AddFileToGroup)]
    [HttpPost("add-file-to-group")]
    public async Task<ActionResult> AddFileToGroup([FromBody] AddFileToChatDto dto, CancellationToken cancellationToken)
    {
        var groupInfo = await _mediator.Send(new GetGroupByNameQuery(dto.OrderId), cancellationToken);
        if (groupInfo.Name != dto.OrderId)
            return NotFound(new ApiResult(string.Empty, ApiResultStatusCode.NotFound, false));

        var newFileName = await _fileService.AddFileAsync(PathConstants.ChatFilePath, dto.File, cancellationToken);
        var message = new CreateMessageDto(dto.OrderId, PathConstants.ChatFilePath + "/" + newFileName);
        await _chatHub.Clients.Groups(dto.OrderId).SendAsync("NewFile", message, cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.ReportGroup)]
    [HttpPatch("report-group")]
    public async Task<ActionResult> ReportGroup([FromBody] ReportGroupDto dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new ReportGroupCommand(dto.Id, _currentUserService.Username, _currentUserService.UserId, dto.ReportReason),
            cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.DeleteChatFileByUrl)]
    [HttpDelete("delete-chat-file-by-url")]
    public async Task<ActionResult> DeleteChatFileByUrl([FromBody] DeleteChatFileByUrlCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}