using Nutritionist.Application.NutritionistOrders.V1.Commands.CreateNutritionistOrder;

namespace Nutritionist.Application.Consumers;

public class OrderedConsumer : IConsumer<Ordered>
{
    private readonly IMediator _mediator;

    public OrderedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<Ordered> context)
    {
        await _mediator.Send(new CreateNutritionistOrderCommand(context.Message.UserId, context.Message.NutritionistId, context.Message.PackageId, context.Message.OrderId,
            context.Message.PackageNamePersian, context.Message.PackageNameEnglish, context.Message.PackageNameArabic, context.Message.Username, context.Message.ImageProfileUserName,
            context.Message.DietCategoryNamePersian, context.Message.DietCategoryNameEnglish, context.Message.DietCategoryNameArabic));
    }
}