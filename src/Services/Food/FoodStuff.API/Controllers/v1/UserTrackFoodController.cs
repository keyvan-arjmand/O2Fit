using AutoMapper;
using Data.Contracts;
using Data.Repositories;
using FoodStuff.API.Models;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.v1.Command.CreatUserTrackFoodCommand;
using FoodStuff.Service.v1.Query.GetUserMealsByDate;
using FoodStuff.Service.v1.Query.GetUserTrackFoodHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class UserTrackFoodController : BaseController
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserTrackFoodRepository _userTrackFoodRepository;
        private readonly IUserTrackNutrientRepository _userTrackNutrientRepository;
        private readonly IRepository<NutrientMeasureUnit> _nutrientMeasureUnitRepository;

        public UserTrackFoodController(IMediator mediator, IMapper mapper, IUserTrackFoodRepository userTrackFoodRepository,
            IUserTrackNutrientRepository userTrackNutrientRepository, IRepository<NutrientMeasureUnit> nutrientMeasureUnitRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userTrackFoodRepository = userTrackFoodRepository;
            _userTrackNutrientRepository = userTrackNutrientRepository;
            _nutrientMeasureUnitRepository = nutrientMeasureUnitRepository;
        }


        [HttpPost]
        public async Task<ApiResult<UserTrackFoodModelDTO>> Post(UserTrackFoodDTO userTrackFoodDTO, CancellationToken cancellationToken)
        {

            UserTrackFood userTrackFood = userTrackFoodDTO.ToEntity(_mapper);
            userTrackFood._id = userTrackFoodDTO._id;
            userTrackFood.FoodNutrientValue = StringConvertor.DoubleToString(userTrackFoodDTO.FoodNutrientValue);
            userTrackFood.FoodMeal = userTrackFoodDTO.FoodMeal;
            UserTrackFoodModelDTO userTrackFoodModelDTO = await _mediator.Send(new UserTrackFoodCommand
            {
                LanguageName = LanguageName == null ? "Persian" : LanguageName,
                userTrackFood = userTrackFood,
            }, cancellationToken);
            return userTrackFoodModelDTO;
        }


        [HttpPut]
        public async Task<ApiResult<UserTrackFoodModelDTO>> Put(UserTrackFoodDTO userTrackFoodDTO, CancellationToken cancellationToken)
        {
            UserTrackFood oldUserTrackFood = await _userTrackFoodRepository.GetByIdAsync(cancellationToken, userTrackFoodDTO.Id);
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

        /// <summary>
        /// ثبت کالری مستقیم
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("Calorie")]
        public async Task<ApiResult<UserTrackFoodModelDTO>> PostByCalorie(UserTrackFoodByCalorieDTO model, CancellationToken cancellationToken)
        {

            var userTrackFood = new UserTrackFood()
            {
                FoodMeal = model.FoodMeal,
                InsertDate = model.InsertDate,
                Value = (float)model.FoodNutrientValue[23],
                MeasureUnitId = 1,
                UserId = model.UserId,
                _id = model._id,
                FoodNutrientValue = StringConvertor.DoubleToString(model.FoodNutrientValue)
            };

            userTrackFood.Id = await _userTrackFoodRepository.AddAsync(userTrackFood, cancellationToken);

            //------------------------------------add user Track Nutrient-------------------------------------------------------------------
            var _userTrackNutrient = await _userTrackNutrientRepository.GetByDate(userTrackFood.UserId, userTrackFood.InsertDate, cancellationToken);
            var userTrackNutrient = new UserTrackNutrient()
            {
                InsertDate = userTrackFood.InsertDate,
                UserId = userTrackFood.UserId,
            };
            if (_userTrackNutrient != null)
            {
                var newNutrients = model.FoodNutrientValue;
                var lastNutrients = StringConvertor.ToNumber(_userTrackNutrient.Value);
                var nutVal = new List<double>();
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

                userTrackNutrient._id = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                userTrackNutrient.Value = userTrackFood.FoodNutrientValue;
                await _userTrackNutrientRepository.AddAsync(userTrackNutrient, cancellationToken);
            }
            //-------------------------------------------------------------------------------------------
            var translatinIds = new List<int>();
            translatinIds.Add(6);
            translatinIds.Add(7);

            var names = await _mediator.Send(new GetTranslationQuery() { Ids = translatinIds, Language = LanguageName });
            //-------------------------------------------------------------------------------------
            UserTrackFoodModelDTO userTrackFoodModelDTO = new UserTrackFoodModelDTO()
            {
                Id = userTrackFood.Id,
                FoodId = null,
                PersonalFoodId = null,
                FoodName = names.Find(f => f.Value == 7).Text,
                MeasureUnitId = userTrackFood.MeasureUnitId,
                MeasureUnitName = names.Find(m => m.Value == 6).Text,
                Value = userTrackFood.Value,
                FoodMeal = userTrackFood.FoodMeal,
                FoodNutrientValue = userTrackFood.FoodNutrientValue,
                InsertDate = userTrackFood.InsertDate,
                UserId = userTrackFood.UserId,
                _id = userTrackFood._id
            };
            return userTrackFoodModelDTO;
        }

        /// <summary>
        /// نمایش لیست غذاها بر اساس وعده
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="foodMealId"></param>
        /// <param name="dateTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetByMealId")]
        public async Task<ApiResult<List<UserTrackFoodModelDTO>>> GetByMealId(int userId, int foodMealId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var _userTrackFoods = await _userTrackFoodRepository.GetByMealIdAsync(userId, foodMealId, dateTime, cancellationToken);
            List<int> nameIds = new List<int>();
            List<int> foodNameIds = _userTrackFoods.Where(t => t.FoodId > 0).Select(f => f.Food.NameId).ToList();
            nameIds.AddRange(foodNameIds);
            List<int> measurNameIds = _userTrackFoods.Select(m => m.MeasureUnit.NameId).ToList();
            nameIds.AddRange(measurNameIds);
            nameIds.Add(7);
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = nameIds, Language = LanguageName });

            var result = new List<UserTrackFoodModelDTO>();
            result = _userTrackFoods.Select(f => new UserTrackFoodModelDTO()
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


        /// <summary>
        /// نمایش لیست غذاهای خورده شده کاربر 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="foodMealId"></param>
        /// <param name="dateTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("UserMealsByDate")]
        public async Task<ApiResult<List<UserTrackFoodModelDTO>>> GetUserMealsByDate(int userId, DateTime dateTime)
        {
            List<UserTrackFoodModelDTO> result = await _mediator.Send(new GetUserMealsByDateQuery
            {
                dateTime = dateTime,
                LanguageName = LanguageName == null ? "Persian" : LanguageName,
                userId = userId
            });
            return result;
        }


        [HttpGet]
        public async Task<ApiResult<UserTrackFoodModelDTO>> Get(int userTrackFoodId, CancellationToken cancellationToken)
        {
            var _userTrackFood = await _userTrackFoodRepository.GetByIdAsNoTrackingAsync(cancellationToken, userTrackFoodId);
            List<int> nameIds = new List<int>();
            if (_userTrackFood.FoodId > 0) { nameIds.Add(_userTrackFood.Food.NameId); }
            if (_userTrackFood.FoodId == null && _userTrackFood.PersonalFoodId == null) { nameIds.Add(7); }
            nameIds.Add(_userTrackFood.MeasureUnit.NameId);
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = nameIds, Language = LanguageName });
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
        public async Task<ApiResult> Delete(int userTrackFoodId, CancellationToken cancellationToken)
        {

            var userTrackFood = await _userTrackFoodRepository.GetByIdAsync(cancellationToken, userTrackFoodId);
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

        [HttpGet("GetIntakeNutrients")]
        public async Task<ApiResult<UserTrackNutrientSelectDTO>> GetIntakeNutrients(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var _userTrackNutrient = new UserTrackNutrient();
            _userTrackNutrient = await _userTrackNutrientRepository.GetByDate(userId, dateTime, cancellationToken);
            if (_userTrackNutrient == null)
            {
                // _userTrackNutrientRepository.Detach(_userTrackNutrient);
                _userTrackNutrient = new UserTrackNutrient()
                {
                    Value = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
                    InsertDate = dateTime,
                    UserId = userId
                };
                //await _userTrackNutrientRepository.AddAsync(_userTrackNutrient, cancellationToken);
            }
            var nutrientsValue = StringConvertor.ToNumber(_userTrackNutrient.Value);
            //------------------------------Translatin----------------------------
            var nutrientMeasureUnit = await _nutrientMeasureUnitRepository.Table.Include(m => m.MeasureUnit).ToListAsync(cancellationToken);
            var translatinIds = new List<int>();
            foreach (var item in nutrientMeasureUnit)
            {
                translatinIds.Add(item.MeasureUnit.NameId);
            }
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = translatinIds, Language = LanguageName });
            //---------------------------------------------------------------------
            var userTrackNutrients = new UserTrackNutrientSelectDTO();
            var userTrackNutrientList = new List<UserTrackNutrientModel>();
            int n = 1;
            foreach (var item in nutrientsValue)
            {
                var NutMeasureUnit = nutrientMeasureUnit.Where(m => m.Nutrient == (Nutrient)n).First();
                var nut = new UserTrackNutrientModel()
                {
                    Value = item,
                    nutrient = new SelectOption<int> { Text = "منتظر ترجمه enum Nutrient", Value = n },
                    MeasureUnitName = names.Find(m => m.Value == NutMeasureUnit.MeasureUnit.NameId).Text,
                    MeasureUnitId = NutMeasureUnit.MeasureUnitId
                };
                n++;
                userTrackNutrientList.Add(nut);
            }
            userTrackNutrients.userTrackNutrient = userTrackNutrientList;
            userTrackNutrients.InsertDate = _userTrackNutrient.InsertDate;
            return userTrackNutrients;
        }


        [HttpGet("GetNutrientsFullReport")]
        public async Task<ApiResult<List<ReportNutrientModel>>> GetNutrientsFullReport(int userId, int days, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now.AddDays(-days);
            var _userTrackNutrientList = await _userTrackNutrientRepository.GetByDateReport(userId, dateTime, cancellationToken);

            //------------------------------Translatin----------------------------
            var nutrientMeasureUnit = await _nutrientMeasureUnitRepository.Table.Include(m => m.MeasureUnit).ToListAsync(cancellationToken);
            var translatinIds = new List<int>();
            foreach (var item in nutrientMeasureUnit)
            {
                translatinIds.Add(item.MeasureUnit.NameId);
            }
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = translatinIds, Language = LanguageName });
            //---------------------------------------------------------------------
            var reportList = new List<ReportNutrientModel>();
            for (int i = 1; i <= 34; i++)
            {
                var NutMeasureUnit = nutrientMeasureUnit.Where(m => m.Nutrient == (Nutrient)i).First();
                var report = new ReportNutrientModel()
                {
                    Nutrient = new SelectOption<int> { Text = "ترجمه Enum Nutrients", Value = i },
                    MeasureUnit = new SelectOption<int>
                    {
                        Text = names.Find(m => m.Value == NutMeasureUnit.MeasureUnit.NameId).Text,
                        Value = NutMeasureUnit.MeasureUnitId
                    }
                };
                var rep = new List<DateAndValuBase<double>>();
                foreach (var item in _userTrackNutrientList)
                {
                    var nutrientsValue = StringConvertor.ToNumber(item.Value);
                    var valueInDate = new DateAndValuBase<double>()
                    {
                        date = item.InsertDate.Date,
                        Value = nutrientsValue[i - 1]
                    };
                    rep.Add(valueInDate);
                }
                report.ValuInDates = rep;
                reportList.Add(report);
            }

            return reportList;
        }

        [HttpGet("GetNutrientsReport")]
        public async Task<ApiResult<List<ReportNutrientModel>>> GetNutrientsReport(int userId, int days, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now.AddDays(-days);
            var _userTrackNutrientList = await _userTrackNutrientRepository.GetByDateReport(userId, dateTime, cancellationToken);

            //------------------------------Translatin----------------------------
            var nutrientMeasureUnit = await _nutrientMeasureUnitRepository.Table.Include(m => m.MeasureUnit).ToListAsync(cancellationToken);
            var translatinIds = new List<int>();
            foreach (var item in nutrientMeasureUnit)
            {
                translatinIds.Add(item.MeasureUnit.NameId);
            }
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = translatinIds, Language = LanguageName });
            //---------------------------------------------------------------------
            var reportList = new List<ReportNutrientModel>();
            for (int i = 1; i <= 34; i++)
            {
                var NutMeasureUnit = nutrientMeasureUnit.Where(m => m.Nutrient == (Nutrient)i).First();
                var report = new ReportNutrientModel()
                {
                    Nutrient = new SelectOption<int> { Text = "ترجمه Enum Nutrients", Value = i },
                    MeasureUnit = new SelectOption<int>
                    {
                        Text = names.Find(m => m.Value == NutMeasureUnit.MeasureUnit.NameId).Text,
                        Value = NutMeasureUnit.MeasureUnitId
                    }
                };
                var rep = new List<DateAndValuBase<double>>();
                foreach (var item in _userTrackNutrientList)
                {
                    var nutrientsValue = StringConvertor.ToNumber(item.Value);
                    var valueInDate = new DateAndValuBase<double>()
                    {
                        date = item.InsertDate.Date,
                        Value = nutrientsValue[i - 1]
                    };
                    rep.Add(valueInDate);
                }
                report.ValuInDates = rep;
                reportList.Add(report);
            }

            return reportList;
        }

        [HttpGet("UserHistory")]
        public async Task<ApiResult<List<UserTrackFoodModelDTO>>> GetUserTrackFoodHistory(int userId, int days, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now.AddDays(-days);
            return await _mediator.Send(new GetUserTrackFoodHistoryQuery
            {
                dateTime = dateTime,
                userId = userId,
                LanguageName = LanguageName
            });
        }
    }
}
