using Common.Constants.Discount;
using Discount.Domain.ValueObjects;
using MongoDB.Driver;
using System.Threading;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;

namespace Discount.Infrastructure.Persistence.Repositories;

public class DiscountRepository : GenericRepository<DiscountO2Fit>, IDiscountRepository
{
    public DiscountRepository(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) : base(mediator, configuration, currentUserService)
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

    public async Task<string> GenerationInvitationCode()
    {
        string generationCode = GenerateCodeConstants.Code + Guid.NewGuid().ToString().Substring(0, 5);
        bool state = await AnyAsync(x => x.Code == generationCode);
        if (!state) return generationCode;
        while (!state)
        {
            generationCode = string.Empty;
            generationCode = GenerateCodeConstants.Code + Guid.NewGuid().ToString().Substring(0, 5);
            state =await AnyAsync(x => x.Code == generationCode);
        }
        return generationCode;
    }

   
}