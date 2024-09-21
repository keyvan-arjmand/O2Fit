using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query
{
    public class GetIngredientByCodeQueryHandler : IRequestHandler<GetIngredientByCodeQuery, List<GetByCodeIngredientAdminViewModel>>, IScopedDependency
    {
        private readonly IRepository<Ingredient> _ingredientRepository;
        private readonly IRepository<IngredientAllergy> _Repository;

        public GetIngredientByCodeQueryHandler(IRepository<Ingredient> ingredientRepository, IRepository<IngredientAllergy> Repository)
        {
            _ingredientRepository = ingredientRepository;
            _Repository = Repository;
        }

        public async Task<List<GetByCodeIngredientAdminViewModel>> Handle(GetIngredientByCodeQuery request, CancellationToken cancellationToken)
        {
            List<GetByCodeIngredientAdminViewModel> result = await _ingredientRepository.TableNoTracking
                 .Include(t => t.Translation)
                 .Where(i => i.Code == request.Code)
                 .Select(s => new GetByCodeIngredientAdminViewModel
                 {
                     Code = s.Code,
                     Name = s.Translation.Persian,
                     Id = s.Id,
                     IsAllergies = _Repository.TableNoTracking.Any(x=>x.IngredientId==s.Id)
                 })
                 .ToListAsync(cancellationToken);

            return result;

        }
    }
}
