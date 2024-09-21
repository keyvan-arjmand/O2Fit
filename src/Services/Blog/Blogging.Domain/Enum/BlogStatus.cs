using System.Runtime.Serialization;

namespace Blogging.Domain.Enum
{
    public enum BlogStatus
    {
        [EnumMember(Value = "در انتظار تایید")]
        AwaitingConfirmation = 0,
        [EnumMember(Value = "تایید شده")]
        Confirmed = 1,
        [EnumMember(Value = "حذف شده")]
        Deleted = 2,
    }
}