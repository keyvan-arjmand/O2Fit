using Common.Constants.Discount;
using Discount.Domain.Aggregates.DiscountNutritionistAggregate;

namespace Discount.Infrastructure.Persistence.Repositories;

public class DiscountNutritionistRepository : GenericRepository<DiscountNutritionist>, IDiscountNutritionistRepository
{
    public DiscountNutritionistRepository(IMediator mediator, IConfiguration configuration,
        ICurrentUserService currentUserService) : base(mediator, configuration, currentUserService)
    {
    }

    public async Task<string> GenerationCode()
    {
        string generationCode = GenerateCodeConstants.Code + Guid.NewGuid().ToString().Substring(0, 5);
        bool state = await AnyAsync(x => x.Code == generationCode);
        if (!state) return generationCode;
        while (!state)
        {
            generationCode = string.Empty;
            generationCode = GenerateCodeConstants.Code + Guid.NewGuid().ToString().Substring(0, 5);
            state = await AnyAsync(x => x.Code == generationCode);
        }

        return generationCode;
    }
}