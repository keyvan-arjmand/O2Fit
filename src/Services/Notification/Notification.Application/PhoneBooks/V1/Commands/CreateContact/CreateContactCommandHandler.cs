namespace Notification.Application.PhoneBooks.V1.Commands.CreateContact;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateContactCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<PhoneBook>.Filter.Eq(x => x.UserId, ObjectId.Parse(request.UserId));
        var phonebook = await _uow.GenericRepository<PhoneBook>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (phonebook == null)
        {
            var newPhoneBook = new PhoneBook(ObjectId.Parse(request.UserId), request.Username, request.FullName, request.IsNutritionist);
            newPhoneBook.FcmTokens.Add(request.FcmToken);
            await _uow.GenericRepository<PhoneBook>().InsertOneAsync(newPhoneBook, null, cancellationToken);
        }
        else
        {
            var fcmTokenFilter = Builders<PhoneBook>.Filter.Eq(x => x.UserId, ObjectId.Parse(request.UserId));
            fcmTokenFilter &= Builders<PhoneBook>.Filter.AnyEq(x => x.FcmTokens, request.FcmToken);
            var isExits = await _uow.GenericRepository<PhoneBook>()
                .GetSingleDocumentByFilterAsync(fcmTokenFilter, cancellationToken);
            if (isExits != null)
                throw new BadRequestException("fcm is duplicate");
            
            phonebook.FcmTokens.Add(request.FcmToken);
            await _uow.GenericRepository<PhoneBook>().UpdateOneAsync(x => x.Id == phonebook.Id, phonebook,
                new Expression<Func<PhoneBook, object>>[]
                {
                    book => book.FcmTokens
                }, null, cancellationToken);
        }
    }
}