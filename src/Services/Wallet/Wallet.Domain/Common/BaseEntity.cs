namespace Wallet.Domain.Common;

[BsonIgnoreExtraElements]
public abstract class BaseEntity
{
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public bool IsDelete { get; set; } = false;
}