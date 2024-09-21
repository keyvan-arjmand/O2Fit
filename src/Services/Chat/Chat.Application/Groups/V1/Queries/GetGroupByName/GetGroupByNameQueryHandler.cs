namespace Chat.Application.Groups.V1.Queries.GetGroupByName;

public class GetGroupByNameQueryHandler : IRequestHandler<GetGroupByNameQuery, GroupDto>
{
    private readonly IUnitOfWork _uow;

    public GetGroupByNameQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<GroupDto> Handle(GetGroupByNameQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Group>.Filter.Eq(x => x.Name, request.Name);
        var group = await _uow.GenericRepository<Group>().GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.Name);

        return group.ToDto<GroupDto>();
    }
}