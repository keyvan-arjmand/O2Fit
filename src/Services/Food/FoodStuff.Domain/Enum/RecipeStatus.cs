using NRediSearch.QueryBuilder;
using System.Runtime.Serialization;

namespace FoodStuff.Domain.Enum
{
    public enum RecipeStatus
    {
        [EnumMember(Value = "در انتظار تایید")]
        AwaitingConfirmation = 0,
        [EnumMember(Value = "تایید شده")]
        Approved = 1,
        [EnumMember(Value = "رد شده")]
        Rejected = 2
        
    }
}