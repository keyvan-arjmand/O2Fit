using EventBus.Messages.Contracts.Services.Foods;
using EventBus.Messages.Contracts.Services.Foods.Food;
using MassTransit;

namespace Food.V2.Application.Consumers.Foods;

public class GetFoodByIdConsumer : IConsumer<GetFood>
{
    private readonly IUnitOfWork _work;

    public GetFoodByIdConsumer(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Consume(ConsumeContext<GetFood> context)
    {
        var food = await _work.FoodAggregation().GetFoodWithDetailById(context.Message.Id);
        var measure = new List<MeasureUnitFood>();
        food.MeasureUnits.ForEach(x => measure.Add(new MeasureUnitFood
        {
            Id = x.Id,
            Value = x.Value,
            MeasureUnitArabic = x.Name.Arabic,
            MeasureUnitEnglish = x.Name.English,
            MeasureUnitPersian = x.Name.Persian
        }));
        GetFoodResult result = new GetFoodResult
        {
            Id = food.Id,
            FoodNameArabic = food.Name.Arabic,
            FoodNamePersian = food.Name.Persian,
            FoodNameEnglish = food.Name.English,
            NutrientValue = food.NutrientValue,
            FoodCode = food.FoodCode,
            FoodMeal = (int)food.FoodMeals[0],
            FoodType = (int)food.FoodType,
            MeasureUnits = food.MeasureUnits.Select(x => new MeasureUnitFood
            {
                Id = x.Id,
                Value = x.Value,
                MeasureUnitArabic = x.Name.Arabic,
                MeasureUnitEnglish = x.Name.English,
                MeasureUnitPersian = x.Name.Persian
            }).ToList()
        };
        await context.RespondAsync(result);
    }
}