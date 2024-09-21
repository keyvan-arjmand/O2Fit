namespace Blogging.Domain.Common
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
        public bool IsDelete { get; set; } = false;
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}