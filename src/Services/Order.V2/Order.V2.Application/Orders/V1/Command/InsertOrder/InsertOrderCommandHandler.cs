using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Payments.Package;
using EventBus.Messages.Contracts.Services.Wallet;
using EventBus.Messages.Events.Services.Identity.Package;
using EventBus.Messages.Events.Services.Nutritionist.Order;
using MassTransit;
using MongoDB.Bson;
using Order.V2.Application.Common.Exceptions;
using Order.V2.Application.Common.Interfaces.Persistence.UoW;
using Order.V2.Domain.Enums;

namespace Order.V2.Application.Orders.V1.Command.InsertOrder;

public class InsertOrderCommandHandler : IRequestHandler<InsertOrderCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetByIdPackage> _clientPackage;
    private readonly IRequestClient<GetTransactionCompanyById> _clientTransaction;
    private readonly IPublishEndpoint _publishEndpoint;

    public InsertOrderCommandHandler(IUnitOfWork uow, IRequestClient<GetByIdPackage> clientPackage,
        IRequestClient<GetTransactionCompanyById> clientTransaction, IPublishEndpoint publishEndpoint)
    {
        _uow = uow;
        _clientPackage = clientPackage;
        _clientTransaction = clientTransaction;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<string> Handle(InsertOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Domain.Aggregates.OrderAggregate.Order
        {
            Id = ObjectId.GenerateNewId().ToString()!,
            FinalAmount = 0,
            Discount = 0,
            PackageId = ObjectId.Empty,
            Amount = 0,
            InsertDate = DateTime.Now,
            CurrencyId = ObjectId.Empty,
            CurrencyCode = string.Empty,
            DiscountCode = string.Empty,
            FinalState = OrderState.Pending.ToDescription(),
            SaleReferenceId = string.Empty,
            Wage = 0,
            UserType = UserType.User.ToDescription(),
            UserId = ObjectId.Empty,
            TraceNo = string.Empty,
            WalletTransactionId = ObjectId.Empty,
            PaymentTransactionId = string.IsNullOrWhiteSpace(request.PaymentTransactionId)
                ? new ObjectId(string.Empty)
                : ObjectId.Parse(request.PaymentTransactionId),
            CountryId = 0,
            NutritionistId = ObjectId.Empty,
            DiscountType = DiscountType.DiscountCodeGeneral.ToDescription(),
            PaymentFor = "",
            PackageType = "",
        };

        order.AddDomainEvent(new Ordered
        {
            OrderId = order.Id,
            PackageId = "6548e937111a330eeed01d4a",
            UserId = "653e5d58c6608deb4cff691c",
            NutritionistId = "65463e623819a4452e5202f7",
        });
        await _uow.GenericRepository<Domain.Aggregates.OrderAggregate.Order>()
            .InsertOneAsync(order, null, cancellationToken);
        return order.Id;
        // order = new Domain.Aggregates.OrderAggregate.Order
        // {
        //     FinalAmount = transaction.Message.FinalAmount,
        //     Discount = transaction.Message.Discount,
        //     PackageId = ObjectId.Parse(transaction.Message.PackageId),
        //     Amount = transaction.Message.Amount,
        //     InsertDate = DateTime.Now,
        //     CurrencyId = ObjectId.Parse(transaction.Message.CurrencyId),
        //     CurrencyCode = transaction.Message.CurrencyCode,
        //     DiscountCode = transaction.Message.DiscountCode,
        //     FinalState = OrderState.Pending.ToDescription(),
        //     SaleReferenceId = transaction.Message.SaleReferenceId,
        //     Wage = transaction.Message.Wage,
        //     UserType = UserType.User.ToDescription(),
        //     UserId = ObjectId.Parse(transaction.Message.UserId),
        //     TraceNo = transaction.Message.TraceNo,
        //     WalletTransactionId = ObjectId.Parse(request.WalletTransactionId),
        //     PaymentTransactionId = string.IsNullOrWhiteSpace(request.PaymentTransactionId)
        //         ? new ObjectId(string.Empty)
        //         : ObjectId.Parse(request.PaymentTransactionId),
        //     CountryId = transaction.Message.CountryId,
        //     NutritionistId = new ObjectId(transaction.Message.NutritionistId),
        //     DiscountType = transaction.Message.DiscountType,
        //     PaymentFor = transaction.Message.PaymentFor,
        //     PackageType = transaction.Message.PackageType,
        // };
        // order.AddDomainEvent(new Ordered
        // {
        //     OrderId = order.Id,
        //     PackageId = transaction.Message.PackageId,
        //     UserId = transaction.Message.UserId,
        //     NutritionistId = transaction.Message.NutritionistId,
        //     //todo                                                                           
        // });
        // var transaction = await _clientTransaction.GetResponse<GetTransactionCompanyByIdResult>(
        //     new GetTransactionCompanyById
        //     {
        //         Id = request.WalletTransactionId,
        //     }, cancellationToken);
        // if (string.IsNullOrEmpty(transaction.Message.Id)) throw new NotFoundException("transaction Not Found");
        //
        // var order = new Domain.Aggregates.OrderAggregate.Order();
        //
        // if (transaction.Message.PaymentFor.Equals(PaymentType.PackageNutritionist.ToDescription()))
        // {
        //     order = new Domain.Aggregates.OrderAggregate.Order
        //     {
        //         FinalAmount = transaction.Message.FinalAmount,
        //         Discount = transaction.Message.Discount,
        //         PackageId = ObjectId.Parse(transaction.Message.PackageId),
        //         Amount = transaction.Message.Amount,
        //         InsertDate = DateTime.Now,
        //         CurrencyId = ObjectId.Parse(transaction.Message.CurrencyId),
        //         CurrencyCode = transaction.Message.CurrencyCode,
        //         DiscountCode = transaction.Message.DiscountCode,
        //         FinalState = OrderState.Pending.ToDescription(),
        //         SaleReferenceId = transaction.Message.SaleReferenceId,
        //         Wage = transaction.Message.Wage,
        //         UserType = UserType.User.ToDescription(),
        //         UserId = ObjectId.Parse(transaction.Message.UserId),
        //         TraceNo = transaction.Message.TraceNo,
        //         WalletTransactionId = ObjectId.Parse(request.WalletTransactionId),
        //         PaymentTransactionId = string.IsNullOrWhiteSpace(request.PaymentTransactionId)
        //             ? new ObjectId(string.Empty)
        //             : ObjectId.Parse(request.PaymentTransactionId),
        //         CountryId = transaction.Message.CountryId,
        //         NutritionistId = new ObjectId(transaction.Message.NutritionistId),
        //         DiscountType = transaction.Message.DiscountType,
        //         PaymentFor = transaction.Message.PaymentFor,
        //         PackageType = transaction.Message.PackageType,
        //     };
        //     order.AddDomainEvent(new Ordered
        //     {
        //         OrderId = order.Id,
        //         PackageId = transaction.Message.PackageId,
        //         UserId = transaction.Message.UserId,
        //         NutritionistId = transaction.Message.NutritionistId,
        //         //todo
        //     });
        // }
        // else if (transaction.Message.PaymentFor.Equals(PaymentType.PackageO2.ToDescription()))
        // {
        //     order = new Domain.Aggregates.OrderAggregate.Order
        //     {
        //         Id = ObjectId.GenerateNewId().ToString(),
        //         FinalAmount = transaction.Message.FinalAmount,
        //         Discount = transaction.Message.Discount,
        //         PackageId = ObjectId.Parse(transaction.Message.PackageId),
        //         Amount = transaction.Message.Amount,
        //         InsertDate = DateTime.Now,
        //         CurrencyId = ObjectId.Parse(transaction.Message.CurrencyId),
        //         CurrencyCode = transaction.Message.CurrencyCode,
        //         DiscountCode = transaction.Message.DiscountCode,
        //         FinalState = OrderState.Accepted.ToDescription(),
        //         SaleReferenceId = transaction.Message.SaleReferenceId,
        //         Wage = transaction.Message.Wage,
        //         UserType = UserType.User.ToDescription(),
        //         UserId = ObjectId.Parse(transaction.Message.UserId),
        //         TraceNo = transaction.Message.TraceNo,
        //         WalletTransactionId = ObjectId.Parse(request.WalletTransactionId),
        //         PaymentTransactionId = string.IsNullOrWhiteSpace(request.PaymentTransactionId)
        //             ? new ObjectId(string.Empty)
        //             : ObjectId.Parse(request.PaymentTransactionId),
        //         CountryId = transaction.Message.CountryId,
        //         NutritionistId = new ObjectId(transaction.Message.NutritionistId),
        //         DiscountType = transaction.Message.DiscountType,
        //         PaymentFor = transaction.Message.PaymentFor,
        //         PackageType = transaction.Message.PackageType,
        //     };
        //     order.AddDomainEvent(new UserPackageRegistered
        //     {
        //         PackageType = order.PackageType.ToEnum<PackageType>(),
        //         UserId = transaction.Message.UserId,
        //         ExpireDate = DateTime.Now.AddDays(transaction.Message.DurationPackage)
        //     });
        // }
        //
        // order.Created = DateTime.UtcNow;
        // order.CreatedBy = request.Username;
        // order.CreatedById = ObjectId.Parse(request.UserId);
        // await _uow.GenericRepository<Domain.Aggregates.OrderAggregate.Order>()
        //     .InsertOneAsync(order, null, cancellationToken);
        // if (transaction.Message.PaymentFor.Equals(PaymentType.PackageNutritionist.ToDescription()))
        // {
        //     await _publishEndpoint.Publish<Ordered>(new Ordered
        //     {
        //         OrderId = order.Id,
        //         PackageId = transaction.Message.PackageId,
        //         UserId = transaction.Message.UserId,
        //         NutritionistId = transaction.Message.NutritionistId,
        //     }, cancellationToken).ConfigureAwait(false);
        // }
        // else if (transaction.Message.PaymentFor.Equals(PaymentType.PackageO2.ToDescription()))
        // {
        //     await _publishEndpoint.Publish<UserPackageRegistered>(new UserPackageRegistered
        //     {
        //         PackageType = order.PackageType.ToEnum<PackageType>(),
        //         UserId = transaction.Message.UserId,
        //         ExpireDate = DateTime.Now.AddDays(transaction.Message.DurationPackage)
        //     }, cancellationToken).ConfigureAwait(false);
        // }
        //
        // return order.Id;
    }
}