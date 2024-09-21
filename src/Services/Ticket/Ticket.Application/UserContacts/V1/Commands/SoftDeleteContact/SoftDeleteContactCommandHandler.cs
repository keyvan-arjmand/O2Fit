using Ticket.Application.Common.Exceptions;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Domain.Aggregates.ContactUsAggregate;

namespace Ticket.Application.UserContacts.V1.Commands.SoftDeleteContact;

public class SoftDeleteContactCommandHandler : IRequestHandler<SoftDeleteContactCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteContactCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contactUs = await _work.GenericRepository<ContactUs>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (contactUs == null)
            throw new NotFoundException(nameof(ContactUs), request.Id);
        await _work.GenericRepository<ContactUs>()
            .SoftDeleteByIdAsync(request.Id, contactUs, null, cancellationToken);
    }
}