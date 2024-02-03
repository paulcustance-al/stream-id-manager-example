namespace GenericStreamIdManagerExample;

public interface IReadOnlyStreamIdManager
{
    T GetValue<T>(string key);
    void Load(string streamId);
}