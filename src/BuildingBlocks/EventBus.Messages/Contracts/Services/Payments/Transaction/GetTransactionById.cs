namespace EventBus.Messages.Contracts.Services.Payments.Transaction;

public record GetTransactionById
{
    public string Id { get; init; } 
}