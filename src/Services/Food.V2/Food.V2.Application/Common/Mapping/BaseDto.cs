namespace Food.V2.Application.Common.Mapping;

[BsonIgnoreExtraElements]
public class BaseDto
{
    public string Id { get; set; } = string.Empty;
    [BsonElement("isDelete")] public bool IsDelete { get; set; } = false;
}