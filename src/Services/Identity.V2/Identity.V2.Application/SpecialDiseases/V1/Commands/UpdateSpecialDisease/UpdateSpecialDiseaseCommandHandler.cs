namespace Identity.V2.Application.SpecialDiseases.V1.Commands.UpdateSpecialDisease;

public class UpdateSpecialDiseaseCommandHandler : IRequestHandler<UpdateSpecialDiseaseCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateSpecialDiseaseCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateSpecialDiseaseCommand request, CancellationToken cancellationToken)
    {
        var specialDisease = await _uow.GenericRepository<SpecialDisease>().GetByIdAsync(request.Id, cancellationToken);
        if (specialDisease == null)
            throw new NotFoundException(nameof(SpecialDisease), request.Id);

        specialDisease.Name.Arabic = request.Name.Arabic;
        specialDisease.Name.English = request.Name.English;
        specialDisease.Name.Persian = request.Name.Persian;

        await _uow.GenericRepository<SpecialDisease>().UpdateOneAsync(x => x.Id == request.Id, specialDisease,
            new Expression<Func<SpecialDisease, object>>[]
            {
                x => x.Name
            }, null, cancellationToken);
    }
}