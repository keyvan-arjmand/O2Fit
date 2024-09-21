using Discount.Domain.Aggregates.DiscountNutritionistAggregate;

namespace Discount.Application.Common.Interfaces.Persistence.Repositories;

public interface IDiscountNutritionistRepository: IGenericRepository<DiscountNutritionist>
{
    public Task<string> GenerationCode();
}