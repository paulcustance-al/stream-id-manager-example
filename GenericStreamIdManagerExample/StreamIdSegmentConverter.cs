using System.Globalization;

namespace GenericStreamIdManagerExample;

internal static class StreamIdSegmentConverter
{
    internal static T ConvertToType<T>(string value)
    {
        try
        {
            if (typeof(IConvertible).IsAssignableFrom(typeof(T)))
                return (T)((IConvertible)value).ToType(typeof(T), CultureInfo.InvariantCulture);

            if (typeof(T) == typeof(Guid))
                return (T)(object)Guid.Parse(value);
        }
        catch (Exception e)
        {
            var message = $"Unable to convert string value '{value}' to type {typeof(T).Name}";
            throw new Exception(message, e);
        }

        throw new Exception($"No converter found to convert type {typeof(T).Name}");
    }
}