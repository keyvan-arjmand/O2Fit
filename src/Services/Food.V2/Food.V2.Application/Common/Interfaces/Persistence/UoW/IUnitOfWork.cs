namespace Food.V2.Application.Common.Interfaces.Persistence.UoW;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : AggregateRoot;
    IIngredientRepository IngredientRepository();
    IFoodRepository FoodRepository();
    IFoodAggregation FoodAggregation();

    IDietPackAggregation DietPackAggregation();
    void AddCommand(Func<Task> func);
    Task TransactionAsync();
}