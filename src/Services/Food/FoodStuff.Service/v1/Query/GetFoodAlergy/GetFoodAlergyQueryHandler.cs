using Common;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.GetFoodAlergy
{
    public class GetFoodAlergyQueryHandler : IRequestHandler<GetFoodAlergyQuery,List<GetFoodAlergyQueryResult>>, IScopedDependency
    {
        private readonly IUserFoodAlergyRepository _alergyrepository;
        private readonly IMediator _mediator;
        public GetFoodAlergyQueryHandler(IMediator mediator
            , IUserFoodAlergyRepository alergyrepository)
        {
            _mediator = mediator;
            _alergyrepository = alergyrepository;

        }
        public async Task<List<GetFoodAlergyQueryResult>> Handle(GetFoodAlergyQuery request, CancellationToken cancellationToken)
        {
            var IngList = await _alergyrepository.GetlistUserAlergy(request.UserId, cancellationToken).Include(a => a.Ingredient).ToListAsync(cancellationToken);
            var result = new List<GetFoodAlergyQueryResult>();
            if (IngList.Count() > 0)
            {
                List<int> nameIds = new List<int>();
                foreach (var item in IngList)
                {
                    nameIds.Add(item.Ingredient.NameId);
                }
                var translations = await _mediator.Send(new GetTranslationQuery
                {
                    Ids = nameIds,
                    Language = request.LanguageName
                });
                result = IngList.Select(ing => new GetFoodAlergyQueryResult()
                {
                    Id = ing.Id,
                    IngredientId = ing.IngredientId,
                    UserId = ing.UserId,
                    _id = ing._id,
                    Name = translations.Find(n => n.Value == ing.Ingredient.NameId).Text
                }).ToList();
            }
            return result;
        }
    }
}
