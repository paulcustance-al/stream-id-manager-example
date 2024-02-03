namespace GenericStreamIdManagerExample;

public class StreamIdManager : IReadOnlyStreamIdManager
{
    private readonly string _delimiter;
    private readonly Dictionary<string, (Type Type, int Position)> _streamIdSegmentTypes;
    private string[] _streamIdSegmentValues;

    public StreamIdManager(Dictionary<string, (Type, int)> streamIdSegmentTypes, string delimiter)
    {
        _streamIdSegmentTypes = streamIdSegmentTypes;
        _streamIdSegmentValues = Array.Empty<string>();
        _delimiter = delimiter;
    }

    public T GetValue<T>(string key)
    {
        var segmentFound = _streamIdSegmentTypes.TryGetValue(key, out var segment);
        
        StreamIdManagerGuards.GuardAgainstSegmentKeyNotFound(segmentFound, key);
        StreamIdManagerGuards.GuardAgainstTypeMismatch(typeof(T), segment.Type);

        return StreamIdSegmentConverter.ConvertToType<T>(_streamIdSegmentValues[segment.Position]);
    }

    public void Load(string key)
    {
        _streamIdSegmentValues = key.Split(_delimiter);
    }
}