namespace Advertise.Application.AdminAdvertises.V1.Commands.ChangeAdminAdvertiseStatus;

public class ChangeAdminAdvertiseStatusCommandHandler : IRequestHandler<ChangeAdminAdvertiseStatusCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeAdminAdvertiseStatusCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ChangeAdminAdvertiseStatusCommand request, CancellationToken cancellationToken)
    {
        var adminAdvertise = await _uow.GenericRepository<AdminAdvertise>().GetByIdAsync(request.Id, cancellationToken);
        if (adminAdvertise == null)
            throw new NotFoundException(nameof(AdminAdvertise), request.Id);

        adminAdvertise.Status = request.Status;

        await _uow.GenericRepository<AdminAdvertise>().UpdateOneAsync(x => x.Id == request.Id, adminAdvertise,
            new Expression<Func<AdminAdvertise, object>>[]
            {
                x => x.Status
            }, null, cancellationToken);
    }
}