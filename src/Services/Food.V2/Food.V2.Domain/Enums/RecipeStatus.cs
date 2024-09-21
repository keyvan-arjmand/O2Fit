using System.Runtime.Serialization;

namespace Food.V2.Domain.Enums
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