using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Nationalities.V1.Commands.CreateNationality;

public class CreateNationalityCommandHandler : IRequestHandler<CreateNationalityCommand, string>
{
    private readonly IUnitOfWork _uow;

    public CreateNationalityCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(CreateNationalityCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId) && !await _uow.GenericRepository<Nationality>()
                .AnyAsync(x => x.Id == request.ParentId, cancellationToken))
            throw new NotFoundException($"Parent Not Found");

        var name = request.Translation.MapTo<NationalityTranslation,TranslationDto>();
        name.Id = ObjectId.GenerateNewId().ToString();

        var nationality = new Nationality()
        {
            ParentId = string.IsNullOrWhiteSpace(request.ParentId) ? ObjectId.Empty : ObjectId.Parse(request.ParentId),
            Translation = name,
        };
        await _uow.GenericRepository<Nationality>().InsertOneAsync(nationality, null, cancellationToken);
        return nationality.Id;
    }
}