using System.Transactions;
using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Payments.Package;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Transactions.V1.Command.CreateTransaction;
using Payment.Domain.Aggregates.SequenceAggregate;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.CreateTransactionWallet;

public class CreateTransactionWalletCommandHandler : IRequestHandler<CreateTransactionWalletCommand, long>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;


    public CreateTransactionWalletCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client)
    {
        _uow = uow;
        _client = client;
    }

    public async Task<long> Handle(CreateTransactionWalletCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // var currency = await _client.GetResponse<GetCurrencyByCodeResult>(new GetCurrencyByCode
            // {
            //     CurrencyCode = request.CurrencyCode
            // }, cancellationToken);
            // if (currency.Message.Id == null) throw new NotFoundException("CurrencyName Not Valid");
            //
            // TransactionDietPackage transaction = new TransactionDietPackage
            // {
            //     Amount = request.AmountCharge,
            //     Wage = 0,   
            //     Bank = request.Bank.ToDescription(),
            //     CurrencyCode = currency.Message.CurrencyCode,
            //     Discount = 0,
            //     FinalAmount = request.AmountCharge,
            //     FinalState = PaymentResult.Pending.ToDescription(),
            //     DiscountCode = string.Empty,
            //     BankOrderId = await _uow.SequenceRepository().BankOrderGeneration(cancellationToken),
            //     CurrencyId = new ObjectId(currency.Message.Id),
            //     PaymentFor = PaymentType.WalletCharge.ToDescription(),
            //     UserType = request.UserType.ToDescription(),
            // };
            //
            // await _uow.GenericRepository<TransactionDietPackage>()
            //     .InsertOneAsync(transaction, null, cancellationToken);
            // return transaction.BankOrderId;
            return 1;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}