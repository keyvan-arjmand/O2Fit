namespace Order.V2.Application.Orders.V1.Command.UpdateOrder;

public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().WithMessage("Id Can not be null").NotEmpty()
            .WithMessage("Id Can not be empty");
        RuleFor(x => x.Id).NotNull().WithMessage("Id Can not be null").NotEmpty()
            .WithMessage("Id Can not be empty");
    }
}