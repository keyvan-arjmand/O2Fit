using AutoMapper;
using FoodStuff.Service.Models;
using FoodStuff.Service.v2.Query.Recipe;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace FoodStuff.Service.v2.Query.Ingredient
{
    public class GetIngredientSearchAdminQueryHandler : IRequestHandler<GetIngredientSearchAdminQuery, List<IngredientSearchAdminResultDto>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Ingredient> _repository;
        private readonly IRepository<Domain.Entities.Food.IngredientAllergy> _repositoryAllergy;
        private readonly IMapper _mapper;

        public GetIngredientSearchAdminQueryHandler(IRepository<Domain.Entities.Food.Ingredient> repository, IMapper mapper, IRepository<Domain.Entities.Food.IngredientAllergy> repositoryAllergy)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryAllergy = repositoryAllergy;
        }

        public async Task<List<IngredientSearchAdminResultDto>> Handle(GetIngredientSearchAdminQuery request, CancellationToken cancellationToken)
        {
            var ingredient = await _repository.TableNoTracking
                .Include(x => x.Translation)
                .Where(x => x.Translation.Persian.Contains(request.Name))
                .OrderBy(x => x.Id)
                .Skip((request.Page - 1 ?? 0) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            var ingredientAllergy = await _repositoryAllergy.TableNoTracking.Select(x => x.IngredientId).OrderBy(o => o).ToListAsync(cancellationToken);

            var result = _mapper.Map<List<Domain.Entities.Food.Ingredient>, List<IngredientSearchAdminResultDto>>(ingredient);

            foreach (var item in result)
            {
                if (ingredientAllergy.Contains(item.Id))
                {
                    item.IsAllergies = true;
                }
                else
                {
                    item.IsAllergies = false;
                }
            }

            return result.OrderBy(x => x.Id).ToList();
        }
    }
}