using Discount.Domain.Aggregates.DiscountO2FitAggregate;

namespace Discount.Application.Common.Interfaces.Persistence.Repositories;

public interface IDiscountRepository : IGenericRepository<DiscountO2Fit>
{
    public Task<string> GenerationCode();
    public Task<string> GenerationInvitationCode();

}