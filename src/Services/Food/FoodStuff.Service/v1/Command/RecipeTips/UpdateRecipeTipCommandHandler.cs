using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Query.RecipeTips;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace FoodStuff.Service.v1.Command.RecipeTips
{
    public class UpdateRecipeTipCommandHandler : IRequestHandler<UpdateRecipeTipCommand>, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Food.Tip> _repository;
        private readonly IRepository<Domain.Entities.Food.Recipe> _repositoryRecepie;
        public UpdateRecipeTipCommandHandler(IRepository<Domain.Entities.Food.Tip> repository, IMapper mapper, IRepository<Domain.Entities.Food.Recipe> repositoryrec)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryRecepie = repositoryrec;
        }

        public async Task<Unit> Handle(UpdateRecipeTipCommand request, CancellationToken cancellationToken)
        {
            var recipe = await
                _repositoryRecepie.Table.FirstOrDefaultAsync(x => x.FoodId == request.FoodId, cancellationToken);
            if (recipe == null) throw new BadRequestException();

            var tips = await _repository.Table.Include(x => x.Recipe)
                .Where(x => request.Tips.Select(x => x.Id).ToList().Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var i in tips)
            {
                i.RecipeId = recipe.Id;
            }
            await _repository.UpdateRangeAsync(tips, cancellationToken);
            return Unit.Value;
        }
    }
}