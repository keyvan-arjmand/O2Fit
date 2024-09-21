namespace Identity.V2.Application.SpecialDiseases.V1.Commands.SoftDeleteSpecialDisease;

public class SoftDeleteSpecialDiseaseCommandHandler : IRequestHandler<SoftDeleteSpecialDiseaseCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteSpecialDiseaseCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteSpecialDiseaseCommand request, CancellationToken cancellationToken)
    {
        var specialDisease = await _uow.GenericRepository<SpecialDisease>().GetByIdAsync(request.Id, cancellationToken);
        if (specialDisease == null)
            throw new NotFoundException(nameof(SpecialDisease), request.Id);

        specialDisease.IsDelete = true;
        await _uow.GenericRepository<SpecialDisease>()
            .SoftDeleteByIdAsync(request.Id, specialDisease, null, cancellationToken);
    }
}