using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Nationalities.V1.Commands.SoftDeleteNationality;

public class SoftDeleteNationalityCommandHandler : IRequestHandler<SoftDeleteNationalityCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteNationalityCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteNationalityCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<Nationality>()
            .GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (result == null) throw new NotFoundException($"Nationality Not Found");
        var filter = Builders<Nationality>.Filter.Eq(x => x.ParentId, ObjectId.Parse(result.Id));
        var childList = await _work.GenericRepository<Nationality>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        if (childList.Count > 0)
        {
            foreach (var i in childList)
            {
                i.ParentId = ObjectId.Empty;
                await _work.GenericRepository<Nationality>()
                    .UpdateOneAsync(x => x.Id == i.Id, i,
                        new Expression<Func<Nationality, object>>[]
                        {
                            x => x.ParentId,
                        }, null, cancellationToken);
            }
        }
        result.IsDelete = true;
        await _work.GenericRepository<Nationality>()
            .SoftDeleteByIdAsync(request.Id, result, cancellationToken: cancellationToken);
    }
}