namespace GenericStreamIdManagerExample;

internal static class StreamIdManagerGuards
{
    internal static void GuardAgainstTypeMismatch(Type requested, Type expected)
    {
        if (requested != expected)
            throw new Exception($"The type requested '{requested.Name}' does not match original " +
                                $"type of '{expected.Name}'");
    }

    internal static void GuardAgainstSegmentKeyNotFound(bool segmentFound, string segmentKey)
    {
        if (segmentFound is false)
        {
            throw new Exception($"The requested segment key '{segmentKey}' cannot be found");
        }
    }
}