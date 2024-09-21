using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.v1.Command.RecipeTips;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Command.RecipeSteps
{
    public class CreateRecipeTipCommandHandler : IRequestHandler<CreateRecipeTipCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Tip> _repository;
        private readonly IRepository<Domain.Entities.Food.Recipe> _repositoryrecipe;

        public CreateRecipeTipCommandHandler(IRepository<Tip> repository, IRepository<Domain.Entities.Food.Recipe> repositoryrecipe)
        {
            _repository = repository;
            _repositoryrecipe = repositoryrecipe;
        }

        public async Task<Unit> Handle(CreateRecipeTipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var recipe =
                    await _repositoryrecipe.Table.FirstOrDefaultAsync(x => x.FoodId == request.FoodId,
                        cancellationToken);
                if (recipe != null)
                {
                    List<Tip> tips = new List<Tip>();
                    foreach (var i in request.TipCreate)
                    {
                        tips.Add(new Tip
                        {
                            DateInsert = DateTime.Now,
                            DescriptionId = i.Id,
                            RecipeId = recipe.Id,
                            IsDelete = false,
                        });
                    }
                    await _repository.AddRangeAsync(tips, cancellationToken);
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
                    await _repositoryrecipe.AddAsync(insertRecipe, cancellationToken);

                    List<Tip> tips = new List<Tip>();
                    foreach (var i in request.TipCreate)
                    {
                        tips.Add(new Tip
                        {
                            DateInsert = DateTime.Now,
                            DescriptionId = i.Id,
                            RecipeId = insertRecipe.Id,
                            IsDelete = false,
                        });
                    }
                    await _repository.AddRangeAsync(tips, cancellationToken);
                    return Unit.Value;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}