namespace Chat.Application.ChatFiles.V1.Queries.GetAllChatFiles;

public class GetAllChatFilesQueryHandler : IRequestHandler<GetAllChatFilesQuery, List<ChatFileDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllChatFilesQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<ChatFileDto>> Handle(GetAllChatFilesQuery request, CancellationToken cancellationToken)
    {
        var allChatFiles = await _uow.GenericRepository<ChatFile>().GetAllAsync(cancellationToken);

        return allChatFiles.ToDto<ChatFileDto>().ToList();
    }
}