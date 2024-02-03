namespace GenericStreamIdManagerExample;

public class ProcedureNoteStreamIdManager
{
    private readonly IReadOnlyStreamIdManager _streamIdManager = new StreamIdManagerBuilder()
        .WithDelimiter("|")
        .WithTypeAndKey<Guid>("PatientId")
        .WithTypeAndKey<Guid>("OrganisationId")
        .WithTypeAndKey<int>("ProcedureNoteId")
        .Build();

    public ProcedureNoteStreamIdManager(string streamId)
    {
        _streamIdManager.Load(streamId);
    }

    public Guid GetPatientId()
    {
        return _streamIdManager.GetValue<Guid>("PatientId");
    }

    public Guid GetOrganisationId()
    {
        return _streamIdManager.GetValue<Guid>("OrganisationId");
    }

    public int GetProcedureNoteId()
    {
        return _streamIdManager.GetValue<int>("ProcedureNoteId");
    }

    public T GetValue<T>(string key)
    {
        return _streamIdManager.GetValue<T>(key);
    }
}