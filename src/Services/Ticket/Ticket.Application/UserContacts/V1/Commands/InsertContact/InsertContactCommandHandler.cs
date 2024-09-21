using MongoDB.Bson;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Domain.Aggregates.ContactUsAggregate;

namespace Ticket.Application.UserContacts.V1.Commands.InsertContact;

public class InsertContactCommandHandler : IRequestHandler<InsertContactCommand>
{
    private readonly IUnitOfWork _work;

    public InsertContactCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(InsertContactCommand request, CancellationToken cancellationToken)
    {
        var contactUs = new ContactUs
        {
          Message = request.Message,
          Title = request.Title,
          Email = request.Email,
          IsRead = request.IsRead,
          Created = DateTime.UtcNow,
          CreatedById = ObjectId.Empty
        };
        await _work.GenericRepository<ContactUs>().InsertOneAsync(contactUs, null, cancellationToken);
    }
}