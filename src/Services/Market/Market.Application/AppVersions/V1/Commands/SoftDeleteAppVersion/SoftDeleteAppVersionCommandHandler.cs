using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Domain.Aggregates.AppVersionAggregate;

namespace Market.Application.AppVersions.V1.Commands.SoftDeleteAppVersion;

public class SoftDeleteAppVersionCommandHandler : IRequestHandler<SoftDeleteAppVersionCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteAppVersionCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteAppVersionCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppVersion>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"app version not found {request.Id}");
        result.IsDelete = true;
        await _work.GenericRepository<AppVersion>()
            .SoftDeleteByIdAsync(request.Id, result, null, cancellationToken);
    }
}