namespace Identity.V2.Domain.Common.User;


public abstract class UserBaseEntity : MongoUser
{
    public bool IsDelete { get; set; }
}