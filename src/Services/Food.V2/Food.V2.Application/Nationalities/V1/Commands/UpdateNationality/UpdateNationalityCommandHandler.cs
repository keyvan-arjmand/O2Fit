using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Nationalities.V1.Commands.UpdateNationality;

public class UpdateNationalityCommandHandler : IRequestHandler<UpdateNationalityCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateNationalityCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateNationalityCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId) && !await _uow.GenericRepository<Nationality>()
                .AnyAsync(x => x.Id == request.ParentId, cancellationToken))
            throw new NotFoundException($"Parent Not Found");
        var result = await _uow.GenericRepository<Nationality>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"Nationality Not Found");

        result.Translation.Arabic = request.Translation.Arabic;
        result.Translation.English = request.Translation.English;
        result.Translation.Persian = request.Translation.Persian;
        result.ParentId = request.ParentId.StringToObjectId();
        await _uow.GenericRepository<Nationality>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<Nationality, object>>[]
                {
                    x => x.Translation,
                    x => x.ParentId
                }, null, cancellationToken);
    }
}