namespace Food.V2.Application.DietPacks.V1.Commands.SoftDeleteDietPack;

public class SoftDeleteDietPackCommandHandler : IRequestHandler<SoftDeleteDietPackCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteDietPackCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteDietPackCommand request, CancellationToken cancellationToken)
    {
        var dietPack = await _uow.GenericRepository<DietPack>().GetByIdAsync(request.Id, cancellationToken);
        if (dietPack == null)
            throw new NotFoundException(nameof(DietPack), request.Id);

        await _uow.GenericRepository<DietPack>().SoftDeleteByIdAsync(request.Id, dietPack, null, cancellationToken);
    }
}