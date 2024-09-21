using Mongo.Migration.Documents;
using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Domain.Aggregates.FavoriteFoodAggregate;
using Track.Domain.ValueObjects;

namespace Track.Application.Common.Utilities;

public static class Helpers
{
    public static int StringToInt(this string id)
    {
        if (!string.IsNullOrEmpty(id) && !int.TryParse(id, out _))
            throw new AppException("Id Not Valid");
        return string.IsNullOrEmpty(id) ? 0 : int.Parse(id);
    }
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

    public static string GetNameInTranslation(this FavoriteFoodTranslation translation, string lang)
    {
        switch (lang)
        {
            case "Persian":
                return translation.Persian;
            case "English":
                return translation.English;
            case "Arabic":
                return translation.Arabic;
            default:
                return translation.Persian;
        }
    }

    public static NotNegativeForDecimalTypes CreateNotNegativeForDecimalType(this decimal value)
    {
        return new NotNegativeForDecimalTypes(value);
    }

    public static decimal GetNotNegativeForDecimalType(this NotNegativeForDecimalTypes value)
    {
        return value.Value;
    }

    public static List<NotNegativeForDecimalTypes> CreateNotNegativeForDecimalType(this List<decimal> valueList)
    {
        List<NotNegativeForDecimalTypes> objectValues = new List<NotNegativeForDecimalTypes>();
        valueList.ForEach(x => objectValues.Add(new NotNegativeForDecimalTypes(x)));
        return objectValues;
    }

    public static List<decimal> GetValueNotNegativeForDecimalType(this List<NotNegativeForDecimalTypes> values)
    {
        List<decimal> Values = new List<decimal>();
        values.ForEach(x => Values.Add(x.Value));
        return Values;
    }

    public static DocumentVersion ToDocumentVersion(this string version)
    {
        var doc = version.Split(".");
        if (version.Split(".").Length != 3) throw new AppException("Version not valid");
        int major = Convert.ToInt16(doc[0]);
        int minor = Convert.ToInt16(doc[1]);
        int revision = Convert.ToInt16(doc[2]);
        return new DocumentVersion(major, minor, revision);
    }

    public static DateTime ToUtcType(this DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}