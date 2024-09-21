using Market.Application.Common.Exceptions;
using MongoDB.Bson;

namespace Market.Application.Common.Utilities;

public static class Helpers
{
    public static ObjectId StringToObjectId(this string id)
    {
        if (!string.IsNullOrEmpty(id) && !ObjectId.TryParse(id, out _))
            throw new AppException("Id Not Valid");
        return string.IsNullOrEmpty(id) ? ObjectId.Empty : ObjectId.Parse(id);
    }

    public static string ObjectIdToString(this ObjectId? id)
    {
        if (id.Equals(ObjectId.Empty))
        {
            return string.Empty;
        }
        else
        {
            return id.ToString();
        }
    }
}