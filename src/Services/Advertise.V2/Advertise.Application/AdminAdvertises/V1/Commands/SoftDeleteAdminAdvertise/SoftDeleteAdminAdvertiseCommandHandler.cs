namespace Advertise.Application.AdminAdvertises.V1.Commands.SoftDeleteAdminAdvertise;

public class SoftDeleteAdminAdvertiseCommandHandler : IRequestHandler<SoftDeleteAdminAdvertiseCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteAdminAdvertiseCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteAdminAdvertiseCommand request, CancellationToken cancellationToken)
    {
        var adminAdvertise = await _uow.GenericRepository<AdminAdvertise>().GetByIdAsync(request.Id, cancellationToken);
        if (adminAdvertise == null)
            throw new NotFoundException(nameof(adminAdvertise), request.Id);
        await _uow.GenericRepository<AdminAdvertise>()
            .SoftDeleteByIdAsync(request.Id, adminAdvertise, null, cancellationToken);

    }
}