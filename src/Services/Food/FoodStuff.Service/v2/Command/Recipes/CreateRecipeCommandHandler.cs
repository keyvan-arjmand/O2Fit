using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v2.Command.Recipes
{
    public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, int>, IScopedDependency
    {
        private readonly IRepository<Recipe> _repository;

        public CreateRecipeCommandHandler(IRepository<Recipe> repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var recipe = new Recipe
                {
                    DateInsert = DateTime.Now,
                    FoodId = request.FoodId,
                    Status = request.Status
                };
                await _repository.AddAsync(recipe, cancellationToken);

                return recipe.Id;
            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError);
            }
        }
    }
}