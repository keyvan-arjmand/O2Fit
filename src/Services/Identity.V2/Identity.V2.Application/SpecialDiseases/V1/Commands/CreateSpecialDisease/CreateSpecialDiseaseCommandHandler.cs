namespace Identity.V2.Application.SpecialDiseases.V1.Commands.CreateSpecialDisease;

public class CreateSpecialDiseaseCommandHandler : IRequestHandler<CreateSpecialDiseaseCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateSpecialDiseaseCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateSpecialDiseaseCommand request, CancellationToken cancellationToken)
    {
        var filter =
            Builders<SpecialDisease>.Filter.Eq(x => x.Name.Persian,
                request.Name.Persian);

        filter &= Builders<SpecialDisease>.Filter.Eq(x => x.IsDelete, false);
        var result = await _uow.GenericRepository<SpecialDisease>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);

        if (result != null)
            throw new BadRequestException("duplicate disease");

        var diseaseName = request.Name.ToEntity<SpecialDiseaseTranslation>();

        await _uow.GenericRepository<SpecialDisease>().InsertOneAsync(new SpecialDisease
        {
            Name = diseaseName
        },null, cancellationToken);
    }
}