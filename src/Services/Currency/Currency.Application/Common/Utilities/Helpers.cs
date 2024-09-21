using Currency.Application.Common.Exceptions;
using MongoDB.Bson;

namespace Currency.Application.Common.Utilities;

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
        return id.Equals(ObjectId.Empty) ? string.Empty : id.ToString()!;
    }

    public static List<ObjectId> StringToObjectIds(this List<string> ids)
    {
        List<ObjectId> objectIds = new List<ObjectId>();
        ids.ForEach(id =>
        {
            if (!string.IsNullOrEmpty(id) && !ObjectId.TryParse(id, out _))
                throw new AppException("Id Not Valid");
            objectIds.Add(string.IsNullOrEmpty(id) ? ObjectId.Empty : id.StringToObjectId());
        });
        return objectIds;
    }

    public static List<string> ObjectIdsToString(this List<ObjectId>? ids)
    {
        List<string> stringIds = new List<string>();
        ids.ForEach(id => { stringIds.Add(!id.Equals(ObjectId.Empty) ? id.ToString()! : string.Empty); });
        return stringIds;
    }
}