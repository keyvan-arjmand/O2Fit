using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.v2.Query.UserTrackFood;
using MediatR;
using Service.v1.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command.CreatUserTrackFoodCommand
{
    class UserTrackFoodCommandHandller : IRequestHandler<UserTrackFoodCommand, UserTrackFoodModelDTO>, IScopedDependency
    {
        private readonly IUserTrackFoodRepository _userTrackFoodRepository;
        public readonly IUserTrackNutrientRepository _userTrackNutrientRepository;
        public readonly IRepository<NutrientMeasureUnit> _nutrientMeasureUnitRepository;
        private readonly IMediator _mediator;
        public UserTrackFoodCommandHandller(IUserTrackFoodRepository userTrackFoodRepository,
            IRepository<NutrientMeasureUnit> nutrientMeasureUnitRepository,
            IUserTrackNutrientRepository userTrackNutrientRepository,
            IMediator mediator)
        {
            _userTrackFoodRepository = userTrackFoodRepository;
            _userTrackNutrientRepository = userTrackNutrientRepository;
            _nutrientMeasureUnitRepository = nutrientMeasureUnitRepository;
            _mediator = mediator;
        }
        public async Task<UserTrackFoodModelDTO> Handle(UserTrackFoodCommand request, CancellationToken cancellationToken)
        {
            request.userTrackFood.Id = await _userTrackFoodRepository.AddAsync(request.userTrackFood,cancellationToken);
            
            //------------------------------------add user Track Nutrient-------------------------------------------------------------------
            var _userTrackNutrient = await _userTrackNutrientRepository.
                GetByDate(request.userTrackFood.UserId, request.userTrackFood.InsertDate, cancellationToken);

            var userTrackNutrient = new UserTrackNutrient()
            {
                InsertDate = request.userTrackFood.InsertDate,
                UserId = request.userTrackFood.UserId
            };
            if (_userTrackNutrient != null)
            {
                List<double> newNutrients = StringConvertor.ToNumber(request.userTrackFood.FoodNutrientValue);
                List<double> lastNutrients = StringConvertor.ToNumber(_userTrackNutrient.Value);
                var nutVal = new List<double>();
                for (int i = 0; i < 33; i++)
                {
                    nutVal.Add(newNutrients[i] + lastNutrients[i]);
                }
                userTrackNutrient.Value = StringConvertor.DoubleToString(nutVal);
                userTrackNutrient.Id = _userTrackNutrient.Id;
                userTrackNutrient._id = _userTrackNutrient._id;
                await _userTrackNutrientRepository.UpdateAsync(userTrackNutrient, cancellationToken);
            }
            else
            {
                //اگر در هر روز اولین غذایی باشد که ثبت می شود آیدی لوکال غذای خورده شده برای نوتریشن هم ثبت میشود
                userTrackNutrient._id = request.userTrackFood._id;
                userTrackNutrient.Value = request.userTrackFood.FoodNutrientValue;
                await _userTrackNutrientRepository.AddAsync(userTrackNutrient, cancellationToken);
            }
            //----------------------------------------------------------------------------------------------------
            //------------------------------Translatin----------------------------
            //request.userTrackFood = await _userTrackFoodRepository.GetByIdAsync(cancellationToken, request.userTrackFood.Id);

            var utf = await _mediator.Send(new GetUserTrackFoodByIdQuery { Id = request.userTrackFood.Id },cancellationToken);
            
            // var food = request.userTrackFood.Food;
            string foodName;
            // var translatinIds = new List<int>();
            // translatinIds.Add(request.userTrackFood.MeasureUnit.NameId);
            // if (request.userTrackFood.FoodId > 0)
            // {
            //     translatinIds.Add(food.NameId);
            //
            // }
            // else if (request.userTrackFood.PersonalFoodId > 0)
            // {
            //
            // }
            // else
            // {
            //     translatinIds.Add(7);
            // };
            //
            // var names = await _mediator.Send(new GetTranslationQuery() { Ids = translatinIds, Language =request.LanguageName });
            if (request.userTrackFood.FoodId > 0)
            {
                foodName = utf.FoodName;
            }
            else if (request.userTrackFood.PersonalFoodId > 0)
            {
                foodName = utf.PersonalFoodName;
            }
            else
            {
                if (request.LanguageName == "Persian")
                {
                    foodName = "کالری شخصی";
                }
                else
                {
                    if (request.LanguageName == "English")
                    {
                        foodName = "Personal Calorie";
                    }
                    else
                    {
                        foodName = "السعرات الحرارية الشخصية";
                    }
                }
                
            }
            //-------------------------------------------------------------------------------------
            UserTrackFoodModelDTO userTrackFoodModelDTO = new UserTrackFoodModelDTO()
            {
                Id = utf.Id,
                FoodId = (utf.FoodId > 0) ? utf.FoodId : null,
                PersonalFoodId = (utf.PersonalFoodId > 0) ? utf.PersonalFoodId : null,
                FoodName = foodName,
                MeasureUnitId = utf.MeasureUnitId,
                MeasureUnitName = utf.MeasureUnitName,
                Value = utf.Value,
                FoodMeal = utf.FoodMeal,
                FoodNutrientValue = utf.FoodNutrientValue,
                InsertDate = utf.InsertDate,
                UserId = utf.UserId,
                _id = utf._id

            };
            return userTrackFoodModelDTO;
        }
    }
}
