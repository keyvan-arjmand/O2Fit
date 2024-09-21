namespace Notification.Application.PhoneBooks.V1.Queries.GetUserDataFromPhoneBook;

public class GetUserDataFromPhoneBookQueryHandler : IRequestHandler<GetUserDataFromPhoneBookQuery, PhoneBookDto>
{
    private readonly IUnitOfWork _uow;

    public GetUserDataFromPhoneBookQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PhoneBookDto> Handle(GetUserDataFromPhoneBookQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<PhoneBook>.Filter.Eq(x => x.UserId, ObjectId.Parse(request.UserId));
        var result = await _uow.GenericRepository<PhoneBook>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (result == null)
            throw new NotFoundException(nameof(PhoneBook), request.UserId);

        return result.ToDto<PhoneBookDto>();
    }
}