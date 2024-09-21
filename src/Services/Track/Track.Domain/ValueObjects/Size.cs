using System.Xml.Linq;
using Track.Domain.Common;
using Track.Domain.Exceptions.Size;
using Track.Domain.Serializers;

namespace Track.Domain.ValueObjects;
[BsonSerializer(typeof(SizeSerializer))]
public class Size : ValueObject
{
    public Size(double size)
    {
        if (size < 0) throw new ValueSizeNotValidException("Size Not Valid");
        ValueSize = size;
    }
    public static implicit operator double(Size size) => size.ValueSize;
    public static explicit operator Size(double size) => new(size);
    public double ValueSize { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ValueSize;
    }
}