namespace GenericStreamIdManagerExample;

public class StreamIdManagerBuilder
{
    private readonly Dictionary<string, (Type Type, int Position)> _streamIdSegmentTypes = new();
    private string _delimiter = "|";

    public IReadOnlyStreamIdManager Build()
    {
        return new StreamIdManager(_streamIdSegmentTypes, _delimiter);
    }

    public StreamIdManagerBuilder WithDelimiter(string delimiter)
    {
        _delimiter = delimiter;

        return this;
    }

    public StreamIdManagerBuilder WithTypeAndKey<T>(string key)
    {
        var position = _streamIdSegmentTypes.Count;

        _streamIdSegmentTypes.Add(key, (typeof(T), position));

        return this;
    }
}