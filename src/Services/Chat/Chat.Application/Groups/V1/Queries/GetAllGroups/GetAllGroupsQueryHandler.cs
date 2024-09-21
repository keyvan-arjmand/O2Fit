namespace Chat.Application.Groups.V1.Queries.GetAllGroups;

public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, List<GroupDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllGroupsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<GroupDto>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var getAllGroups = await _uow.GenericRepository<Group>().GetAllAsync(cancellationToken);

        return getAllGroups.ToDto<GroupDto>().ToList();
    }
}