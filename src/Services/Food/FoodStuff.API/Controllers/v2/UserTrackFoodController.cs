using AutoMapper;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Service.v2.Query.UserTrackFood;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Common;
using Common.Exceptions;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using Service.v1.Query;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v2
{
    [Authorize]
    [ApiVersion("2")]
    public class UserTrackFoodController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserTrackFoodRepository _userTrackFoodRepository;
        private readonly IUserTrackNutrientRepository _userTrackNutrientRepository;
        private readonly IRepository<NutrientMeasureUnit> _nutrientMeasureUnitRepository;

        public UserTrackFoodController(IMediator mediator, IMapper mapper, IUserTrackFoodRepository userTrackFoodRepository, IUserTrackNutrientRepository userTrackNutrientRepository, IRepository<NutrientMeasureUnit> nutrientMeasureUnitRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userTrackFoodRepository = userTrackFoodRepository;
            _userTrackNutrientRepository = userTrackNutrientRepository;
            _nutrientMeasureUnitRepository = nutrientMeasureUnitRepository;
        }

        [HttpPut]
        public async Task<ApiResult<UserTrackFoodModelDTO>> Put(UserTrackFoodDTO userTrackFoodDTO, CancellationToken cancellationToken)
        {
            UserTrackFood oldUserTrackFood = await _userTrackFoodRepository.GetBy_IdAsync(cancellationToken, userTrackFoodDTO._id);
            if (oldUserTrackFood == null)
            {
                throw new AppException(ApiResultStatusCode.NotFound);
            }
            
            _userTrackFoodRepository.Detach(oldUserTrackFood);
            UserTrackFood userTrackFood = oldUserTrackFood;
            userTrackFood = userTrackFoodDTO.ToEntity(_mapper);
            userTrackFood._id = userTrackFoodDTO._id;
            userTrackFood.Id = oldUserTrackFood.Id;
            userTrackFood.FoodNutrientValue = StringConvertor.DoubleToString(userTrackFoodDTO.FoodNutrientValue);
            userTrackFood.FoodMeal = userTrackFoodDTO.FoodMeal;

            await _userTrackFoodRepository.BasicUpdateAsync(userTrackFood.Id, userTrackFood, cancellationToken);

            //------------------------------------add user Track Nutrient-------------------------------------------------------------------
            if (oldUserTrackFood.InsertDate.Date == userTrackFoodDTO.InsertDate.Date)
            {
                var _userTrackNutrient = await _userTrackNutrientRepository.GetByDate(userTrackFood.UserId, userTrackFood.InsertDate.Date, cancellationToken);
                var userTrackNutrient = new UserTrackNutrient()
                {
                    InsertDate = oldUserTrackFood.InsertDate,
                    UserId = userTrackFood.UserId,
                };

                var newNutrients = userTrackFoodDTO.FoodNutrientValue;
                var lastNutrients = StringConvertor.ToNumber(_userTrackNutrient.Value);
                var nutVal = new List<double>();
                for (int i = 0; i < lastNutrients.Count(); i++)
                {
                    double value = newNutrients[i] + lastNutrients[i] - oldUserTrackFood.FoodNutrientValue[i];
                    double nutValue = (value > 0) ? value : 0;
                    nutVal.Add(nutValue);
                }
                userTrackNutrient.Value = StringConvertor.DoubleToString(nutVal);
                userTrackNutrient.Id = _userTrackNutrient.Id;

                userTrackNutrient._id = _userTrackNutrient._id;
                await _userTrackNutrientRepository.UpdateAsync(userTrackNutrient, cancellationToken);
            }
            else
            {
                //------------------------------------remove user Track Nutrient-------------------------------------------------------------------
                var _userTrackNutrient = await _userTrackNutrientRepository.GetByDate(oldUserTrackFood.UserId, oldUserTrackFood.InsertDate, cancellationToken);
                var userTrackNutrient = new UserTrackNutrient()
                {
                    InsertDate = oldUserTrackFood.InsertDate,
                    UserId = oldUserTrackFood.UserId,
                };
                var foodNutrients = StringConvertor.ToNumber(oldUserTrackFood.FoodNutrientValue);
                var lastNutrients = StringConvertor.ToNumber(_userTrackNutrient.Value);
                var nutVal = new List<double>();
                for (int i = 0; i < lastNutrients.Count(); i++)
                {
                    var value = lastNutrients[i] - foodNutrients[i];
                    double nutValue = (value > 0) ? value : 0;
                    nutVal.Add(nutValue);
                }
                userTrackNutrient.Value = StringConvertor.DoubleToString(nutVal);
                userTrackNutrient.Id = _userTrackNutrient.Id;
                await _userTrackNutrientRepository.UpdateAsync(userTrackNutrient, cancellationToken);
                //-----------------------------------if insert Date Change---------------------------------------------------
                //------------------------------------add user Track Nutrient-------------------------------------------------------------------
                _userTrackNutrient = await _userTrackNutrientRepository.GetByDate(userTrackFood.UserId, userTrackFoodDTO.InsertDate, cancellationToken);
                userTrackNutrient = new UserTrackNutrient()
                {
                    InsertDate = userTrackFoodDTO.InsertDate,
                    UserId = userTrackFood.UserId,
                };
                if (_userTrackNutrient != null)
                {

                    var newNutrients = userTrackFoodDTO.FoodNutrientValue;
                    lastNutrients = StringConvertor.ToNumber(_userTrackNutrient.Value);
                    nutVal = new List<double>();
                    for (int i = 0; i < lastNutrients.Count(); i++)
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
                    userTrackNutrient._id = userTrackFoodDTO._id;
                    userTrackNutrient.Value = userTrackFood.FoodNutrientValue;
                    await _userTrackNutrientRepository.AddAsync(userTrackNutrient, cancellationToken);
                }
            }
            //------------------------------Translatin----------------------------------------------------------------------
            var _userTrackFood = await _userTrackFoodRepository.GetByIdAsync(cancellationToken, userTrackFood.Id);
            List<int> nameIds = new List<int>();
            if (_userTrackFood.FoodId > 0) { nameIds.Add(_userTrackFood.Food.NameId); }
            if (_userTrackFood.FoodId == null && _userTrackFood.PersonalFoodId == null) { nameIds.Add(7); }
            nameIds.Add(_userTrackFood.MeasureUnit.NameId);
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = nameIds, Language = LanguageName });
            //var _userTrackFood = await _mediator.Send(new GetUserTrackFoodByIdQuery { Id = userTrackFood.Id });

            var result = new UserTrackFoodModelDTO()
            {
                Id = _userTrackFood.Id,
                FoodId = (_userTrackFood.FoodId > 0) ? _userTrackFood.FoodId : null,
                PersonalFoodId = (_userTrackFood.PersonalFoodId > 0) ? _userTrackFood.PersonalFoodId : null,
                FoodName = (_userTrackFood.FoodId == null && _userTrackFood.PersonalFoodId == null) ? names.Find(n => n.Value == 7).Text : (_userTrackFood.FoodId > 0) ? names.Find(n => n.Value == _userTrackFood.Food.NameId).Text : _userTrackFood.PersonalFood.Name,
                MeasureUnitId = _userTrackFood.MeasureUnitId,
                MeasureUnitName = names.Find(m => m.Value == _userTrackFood.MeasureUnit.NameId).Text,
                Value = _userTrackFood.Value,
                FoodMeal = _userTrackFood.FoodMeal,
                FoodNutrientValue = _userTrackFood.FoodNutrientValue,
                InsertDate = _userTrackFood.InsertDate,
                UserId = _userTrackFood.UserId,
                _id = _userTrackFood._id
            };
            return result;
        }



        [HttpDelete]
        public async Task<ApiResult> Delete(string _id, CancellationToken cancellationToken)
        {

            var userTrackFood = await _userTrackFoodRepository.GetBy_IdAsync(cancellationToken, _id);
            if (userTrackFood == null)
            {
                throw new AppException(ApiResultStatusCode.NotFound, "UserTrackFood Not Found");
            }
            //------------------------------------remove user Track Nutrient-------------------------------------------------------------------
            var _userTrackNutrient = await _userTrackNutrientRepository.GetByDate(userTrackFood.UserId, userTrackFood.InsertDate, cancellationToken);
            var userTrackNutrient = new UserTrackNutrient()
            {
                InsertDate = userTrackFood.InsertDate,
                UserId = userTrackFood.UserId,
            };
            var foodNutrients = StringConvertor.ToNumber(userTrackFood.FoodNutrientValue);
            var lastNutrients = StringConvertor.ToNumber(_userTrackNutrient.Value);
            var nutVal = new List<double>();
            for (int i = 0; i < lastNutrients.Count(); i++)
            {
                var value = lastNutrients[i] - foodNutrients[i];
                double nutValue = (value > 0) ? value : 0;
                nutVal.Add(value);
            }
            userTrackNutrient.Value = StringConvertor.DoubleToString(nutVal);
            userTrackNutrient.Id = _userTrackNutrient.Id;
            await _userTrackNutrientRepository.UpdateAsync(userTrackNutrient, cancellationToken);
            //------------------------------------Delete user Track Nutrient-------------------------------------------------------------------
            await _userTrackFoodRepository.DeleteAsync(userTrackFood, cancellationToken);

            return Ok();
        }


    }
}
