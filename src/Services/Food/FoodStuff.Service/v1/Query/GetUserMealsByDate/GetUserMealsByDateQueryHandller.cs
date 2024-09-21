using Common;
using Data.Contracts;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace FoodStuff.Service.v1.Query.GetUserMealsByDate
{
    public class GetUserMealsByDateQueryHandller : IRequestHandler<GetUserMealsByDateQuery, List<UserTrackFoodModelDTO>>, IScopedDependency
    {
        private readonly IUserTrackFoodRepository _userTrackFoodRepository;
        public readonly IUserTrackNutrientRepository _userTrackNutrientRepository;
        public readonly IRepository<NutrientMeasureUnit> _nutrientMeasureUnitRepository;
        private readonly IMediator _mediator;
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public GetUserMealsByDateQueryHandller(IUserTrackFoodRepository userTrackFoodRepository,
            IRepository<NutrientMeasureUnit> nutrientMeasureUnitRepository,
            IUserTrackNutrientRepository userTrackNutrientRepository,
            IMediator mediator,
            IDatabaseConnectionFactory connectionFactory)
        {
            _userTrackFoodRepository = userTrackFoodRepository;
            _userTrackNutrientRepository = userTrackNutrientRepository;
            _nutrientMeasureUnitRepository = nutrientMeasureUnitRepository;
            _mediator = mediator;
            _connectionFactory = connectionFactory;
        }
        public async Task<List<UserTrackFoodModelDTO>> Handle(GetUserMealsByDateQuery request, CancellationToken cancellationToken)
        {
            using var conn = await _connectionFactory.CreateConnectionAsync();

            //var _userTrackFoods = await _userTrackFoodRepository.GetUserMealsByDateAsync(request.userId, request.dateTime, cancellationToken);
            // List<int> nameIds = new List<int>();
            // List<int> foodNameIds = _userTrackFoods.Where(t => t.FoodId > 0).Select(f => f.Food.NameId).ToList();
            // nameIds.AddRange(foodNameIds);
            // List<int> measurNameIds = _userTrackFoods.Select(m => m.MeasureUnit.NameId).ToList();
            // nameIds.AddRange(measurNameIds);
            // nameIds.Add(7);
            // var names = await _mediator.Send(new GetTranslationQuery() { Ids = nameIds, Language = request.LanguageName });
            //
            List<UserTrackFoodViewModel> userTrackFoods = new List<UserTrackFoodViewModel>();

            string query = "SELECT u.\"Id\", u.\"UserId\", u.\"Value\"," +
                           " u.\"FoodMeal\",\r\nu.\"InsertDate\"," +
                           " u.\"FoodNutrientValue\",\r\nu.\"MeasureUnitId\"," +
                           $" mu_tr.\"{request.LanguageName}\" MeasureUnitName,\r\n" +
                           "u.\"PersonalFoodId\", fp.\"Name\" PersonalFoodName, u.\"FoodId\", " +
                           $"f_tr.\"{request.LanguageName}\" FoodName,\r\nu._id\r\n" +
                           "FROM \"UserTrackFoods\" AS u\r\n" +
                           "FULL JOIN \"Foods\" AS f ON u.\"FoodId\" = f.\"Id\"\r\n" +
                           "FULL join \"Translations\" f_tr ON f_tr.\"Id\" = f.\"NameId\"\r\n" +
                           "FULL JOIN \"PersonalFoods\" fp ON fp.\"Id\" = u.\"PersonalFoodId\"\r\n" +
                           "FULL join \"MeasureUnits\" ON \"MeasureUnits\".\"Id\" = u.\"MeasureUnitId\"\r\n" +
                           "FULL join \"Translations\" mu_tr ON mu_tr.\"Id\" = \"MeasureUnits\".\"NameId\"\r\n" +
                           $"WHERE u.\"InsertDate\"='{request.dateTime.Date}' AND u.\"UserId\"={request.userId}";

            userTrackFoods =
                conn
                    .Query<UserTrackFoodViewModel>(query).ToList();


            conn.Close();

            List<UserTrackFoodModelDTO> userTrackFoodsResult = new List<UserTrackFoodModelDTO>();
            foreach (var item in userTrackFoods)
            {
                string foodName = "";
                if (item.FoodId > 0)
                {
                    foodName = item.FoodName;
                }
                else if (item.PersonalFoodId > 0)
                {
                    foodName = item.PersonalFoodName;
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
                UserTrackFoodModelDTO userTrackFoodModelDTO = new UserTrackFoodModelDTO
                {
                    Id = item.Id,
                    FoodId = (item.FoodId > 0) ? item.FoodId : null,
                    PersonalFoodId = (item.PersonalFoodId > 0) ? item.PersonalFoodId : null,
                    FoodName = foodName,
                    MeasureUnitId = item.MeasureUnitId,
                    MeasureUnitName = item.MeasureUnitName,
                    Value = item.Value,
                    FoodMeal = item.FoodMeal,
                    FoodNutrientValue = item.FoodNutrientValue,
                    InsertDate = item.InsertDate,
                    UserId = item.UserId,
                    _id = item._id
                };

                userTrackFoodsResult.Add(userTrackFoodModelDTO);
            }

            // var result = new List<UserTrackFoodModelDTO>();
            // result = _userTrackFoods.Select(f => new UserTrackFoodModelDTO()
            // {
            //     Id = f.Id,
            //     FoodId = (f.FoodId > 0) ? f.FoodId : null,
            //     PersonalFoodId = (f.PersonalFoodId > 0) ? f.PersonalFoodId : null,
            //     FoodName = (f.FoodId == null && f.PersonalFoodId == null) ? names.Find(n => n.Value == 7)?.Text : (f.FoodId > 0) ? names.Find(n => n.Value == f.Food.NameId).Text : f.PersonalFood.Name,
            //     MeasureUnitId = f.MeasureUnitId,
            //     MeasureUnitName = names.Find(m => m.Value == f.MeasureUnit.NameId)?.Text,
            //     Value = f.Value,
            //     FoodMeal = f.FoodMeal,
            //     FoodNutrientValue = f.FoodNutrientValue,
            //     InsertDate = f.InsertDate,
            //     UserId = f.UserId,
            //     _id = f._id
            // }).ToList();
            return userTrackFoodsResult;
        }
    }
}
