namespace Notification.Application.PhoneBooks.V1.Queries.GetAllContacts;

public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, List<PhoneBookDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllContactsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<PhoneBookDto>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var data = await _uow.GenericRepository<PhoneBook>().GetAllAsync(cancellationToken);
        return data.ToDto<PhoneBookDto>().ToList();
    }
}