using System.Threading;
using System.Threading.Tasks;
using Common;
using Dapper;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;

namespace FoodStuff.Service.v2.Query.UserTrackFood
{
    public class GetUserTrackFoodByIdQueryHandler : IRequestHandler<GetUserTrackFoodByIdQuery, UserTrackFoodViewModel> , IScopedDependency
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public GetUserTrackFoodByIdQueryHandler(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<UserTrackFoodViewModel> Handle(GetUserTrackFoodByIdQuery request, CancellationToken cancellationToken)
        {
            using var conn = await _connectionFactory.CreateConnectionAsync();

            string query = "SELECT UTF.\"Id\", UTF.\"UserId\", UTF.\"Value\", UTF.\"FoodMeal\"," +
                           "UTF.\"InsertDate\", UTF.\"FoodNutrientValue\",UTF.\"MeasureUnitId\"," +
                           " MU_TR.\"Persian\" MeasureUnitName,UTF.\"PersonalFoodId\", UTF.\"FoodId\"," +
                           " UTF._id,F_TR.\"Persian\" FoodName, PF.\"Name\" PersonalFoodName " +
                           "FROM \"UserTrackFoods\" UTF " +
                           "FULL JOIN \"MeasureUnits\" MU ON MU.\"Id\" = UTF.\"MeasureUnitId\" " +
                           "FULL JOIN \"Translations\" MU_TR ON MU_TR.\"Id\" = MU.\"NameId\" " +
                           "FULL JOIN \"Foods\" F ON F.\"Id\" = UTF.\"FoodId\" " +
                           "FULL JOIN \"Translations\" F_TR ON F_TR.\"Id\" = F.\"NameId\" " +
                           "FULL join \"PersonalFoods\" PF ON PF.\"Id\" = UTF.\"PersonalFoodId\" " +
                           $"WHERE UTF.\"Id\"={request.Id}";

            UserTrackFoodViewModel userTrackFoodViewModel = await conn.QuerySingleOrDefaultAsync<UserTrackFoodViewModel>(query);

            return userTrackFoodViewModel;
        }
    }
}
