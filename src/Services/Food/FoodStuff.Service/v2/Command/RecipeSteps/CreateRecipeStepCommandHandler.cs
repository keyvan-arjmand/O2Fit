using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v2.Command.RecipeSteps
{
    public class CreateRecipeStepCommandHandler: IRequestHandler<CreateRecipeStepCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<RecipeStep> _repository;
        public CreateRecipeStepCommandHandler(IRepository<RecipeStep> repository)
        {
            _repository = repository;
        }


        public async Task<Unit> Handle(CreateRecipeStepCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var step = new RecipeStep
                {
                    DateInsert = DateTime.Now,
                    DescriptionId = request.DescriptionId,
                    RecipeId = request.RecipeId
                };
                await _repository.AddAsync(step, cancellationToken);

                return Unit.Value;

            }
            catch (Exception e)
            {

                throw new AppException(ApiResultStatusCode.ServerError);
            }
        }
    }
}