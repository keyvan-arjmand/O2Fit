namespace Chat.Application.Jobs;

public class DeleteFilesAfterThreeMonthsJob : IJob
{
    private readonly IFileService _fileService;
    private readonly IMediator _mediator;

    public DeleteFilesAfterThreeMonthsJob(IFileService fileService, IMediator mediator)
    {
        _fileService = fileService;
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var allChatFiles = await _mediator.Send(new GetAllChatFilesQuery());
        foreach (var chatFile in allChatFiles.Where(x => (DateTime.UtcNow - x.Created).TotalDays >= 90))
        {
            _fileService.RemoveFile(chatFile.FileName, PathConstants.ChatFilePath);
            await _mediator.Send(new DeleteChatFileByIdCommand(chatFile.Id));
        }
    }
}