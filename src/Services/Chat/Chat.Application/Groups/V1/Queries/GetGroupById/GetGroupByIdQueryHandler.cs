namespace Chat.Application.Groups.V1.Queries.GetGroupById;

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GetGroupByIdDto>
{
    private readonly IUnitOfWork _uow;

    public GetGroupByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<GetGroupByIdDto> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _uow.GenericRepository<Group>().GetByIdAsync(request.Id, cancellationToken);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.Id);

        return group.ToDto<GetGroupByIdDto>();
    }
}