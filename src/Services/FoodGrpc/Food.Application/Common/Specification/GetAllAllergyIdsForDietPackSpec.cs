using Food.Domain.Entities;

namespace Food.Application.Common.Specification;

public class GetAllAllergyIdsForDietPackSpec : Specification<DietPackAlerge>
{
    public GetAllAllergyIdsForDietPackSpec(int[] allergyIds)
    {
        Query.Where(x => allergyIds.Contains(x.IngredientId));
    }
}