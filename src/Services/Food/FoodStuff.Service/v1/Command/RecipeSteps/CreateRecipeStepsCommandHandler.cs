using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Command.RecipeSteps
{
    public class CreateRecipeStepsCommandHandler : IRequestHandler<CreateRecipeStepsCommand>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.RecipeStep> _repositorysteps;
        private readonly IRepository<Domain.Entities.Food.Recipe> _repositoryRecepie;
        public CreateRecipeStepsCommandHandler(IRepository<Domain.Entities.Food.RecipeStep> repositorysteps, IRepository<Domain.Entities.Food.Recipe> repositoryRecepie)
        {
            _repositorysteps = repositorysteps;
            _repositoryRecepie = repositoryRecepie;
        }

        public async Task<Unit> Handle(CreateRecipeStepsCommand request, CancellationToken cancellationToken)
        {
            var recipe =
                await _repositoryRecepie.Table
                    .FirstOrDefaultAsync(x => x.FoodId == request.FoodId,
                    cancellationToken);
            if (recipe != null)
            {
                List<RecipeStep> steps = new List<RecipeStep>();
                foreach (var i in request.StepCreate)
                {
                    steps.Add(new RecipeStep
                    {
                        DateInsert = DateTime.Now,
                        IsDelete = false,
                        RecipeId = recipe.Id,
                        DescriptionId = i.Id
                    });
                }
                await _repositorysteps.AddRangeAsync(steps, cancellationToken);
                return Unit.Value;
            }
            else
            {
                var insertRecipe = new Recipe
                {
                    DateInsert = DateTime.Now,
                    FoodId = request.FoodId,
                    Status = (int)RecipeStatus.AwaitingConfirmation,
                };
                await _repositoryRecepie.AddAsync(insertRecipe, cancellationToken);

                List<RecipeStep> steps = new List<RecipeStep>();
                foreach (var i in request.StepCreate)
                {
                    steps.Add(new RecipeStep
                    {
                        DateInsert = DateTime.Now,
                        IsDelete = false,
                        RecipeId = insertRecipe.Id,
                        DescriptionId = i.Id
                    });
                }
                await _repositorysteps.AddRangeAsync(steps, cancellationToken);
                return Unit.Value;
            }
        }
    }
}