using Common;
using Data.Contracts;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace FoodStuff.Service.v1.Query.GetUserTrackFoodHistory
{
    public class GetUserTrackFoodHistoryQueryHandller : IRequestHandler<GetUserTrackFoodHistoryQuery, List<UserTrackFoodModelDTO>>, IScopedDependency
    {
        private readonly IUserTrackFoodRepository _userTrackFoodRepository;
        public readonly IUserTrackNutrientRepository _userTrackNutrientRepository;
        public readonly IRepository<NutrientMeasureUnit> _nutrientMeasureUnitRepository;
        private readonly IMediator _mediator;
        public GetUserTrackFoodHistoryQueryHandller(IUserTrackFoodRepository userTrackFoodRepository,
            IRepository<NutrientMeasureUnit> nutrientMeasureUnitRepository,
            IUserTrackNutrientRepository userTrackNutrientRepository,
            IMediator mediator)
        {
            _userTrackFoodRepository = userTrackFoodRepository;
            _userTrackNutrientRepository = userTrackNutrientRepository;
            _nutrientMeasureUnitRepository = nutrientMeasureUnitRepository;
            _mediator = mediator;
        }
        public async Task<List<UserTrackFoodModelDTO>> Handle(GetUserTrackFoodHistoryQuery request, CancellationToken cancellationToken)
        {

            //var _userTrackFoods = await _userTrackFoodRepository.Table
            //    .Include(f => f.Food)
            //    .Include(p => p.PersonalFood)
            //    .Include(m => m.MeasureUnit)
            //    .Where(f => f.InsertDate >= dateTime && f.UserId == userId).ToListAsync(cancellationToken);
           //==============OLD Start==================
            // var _userTrackFoods = await (from foodRep in _userTrackFoodRepository.Table
           //                                  .Include(f => f.Food)
           //                                  .Include(p => p.PersonalFood)
           //                                  .Include(m => m.MeasureUnit)
           //                              where (foodRep.InsertDate >= request.dateTime) && (foodRep.UserId == request.userId)
           //                              select foodRep).ToListAsync(cancellationToken);
           //===============Old End========================

           var userTrackFood = await _userTrackFoodRepository.TableNoTracking.Include(x => x.Food).Include(p => p.PersonalFood)
               .Include(m => m.MeasureUnit)
               .Where(x => x.InsertDate >= request.dateTime && x.UserId == request.userId)
               .ToListAsync(cancellationToken);
            List<int> nameIds = new List<int>();
            List<int> foodNameIds = userTrackFood.Where(t => t.FoodId > 0).Select(f => f.Food.NameId).ToList();
            nameIds.AddRange(foodNameIds);
            List<int> measurNameIds = userTrackFood.Select(m => m.MeasureUnit.NameId).ToList();
            nameIds.AddRange(measurNameIds);
            nameIds.Add(7);
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = nameIds, Language = request.LanguageName },cancellationToken);

            var result = userTrackFood.Select(f => new UserTrackFoodModelDTO()
            {
                Id = f.Id,
                FoodId = (f.FoodId > 0) ? f.FoodId : null,
                PersonalFoodId = (f.PersonalFoodId > 0) ? f.PersonalFoodId : null,
                FoodName = (f.FoodId == null && f.PersonalFoodId == null) ? names.Find(n => n.Value == 7).Text : (f.FoodId > 0) ? names.Find(n => n.Value == f.Food.NameId).Text : f.PersonalFood.Name,
                MeasureUnitId = f.MeasureUnitId,
                MeasureUnitName = names.Find(m => m.Value == f.MeasureUnit.NameId).Text,
                Value = f.Value,
                FoodMeal = f.FoodMeal,
                FoodNutrientValue = f.FoodNutrientValue,
                InsertDate = f.InsertDate,
                UserId = f.UserId,
                _id = f._id

            }).ToList();

            return result;
        }
    }
}
