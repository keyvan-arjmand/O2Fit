namespace Discount.Application.Common.Interfaces.Persistence.UoW;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : AggregateRoot;
    IDiscountRepository DiscountRepository();
    IDiscountNutritionistRepository DiscountNutritionistRepository();
    void AddCommand(Func<Task> func);
    Task TransactionAsync();
}