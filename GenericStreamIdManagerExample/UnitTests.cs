using FluentAssertions;

namespace GenericStreamIdManagerExample;

public class UnitTests
{
    [Fact]
    public void Should_RetrievePatientIdAsGuid()
    {
        // Arrange
        var patientId = new Guid("6D9D2DCD-E85B-4EAE-8419-38A94C31D7A3");
        var streamId = $"{patientId}|49AC0617-6A01-453C-A051-5F0709CFF601|12345";
        var streamIdManager = new ProcedureNoteStreamIdManager(streamId);

        // Act
        var patientIdReturned = streamIdManager.GetPatientId();

        // Assert
        patientIdReturned.Should().Be(patientId);
    }

    [Fact]
    public void Should_RetrieveOrganisationIdAsGuid()
    {
        // Arrange
        var organisationId = new Guid("49AC0617-6A01-453C-A051-5F0709CFF601");
        var streamId = $"6D9D2DCD-E85B-4EAE-8419-38A94C31D7A3|{organisationId}|12345";
        var streamIdManager = new ProcedureNoteStreamIdManager(streamId);

        // Act
        var organisationIdReturned = streamIdManager.GetOrganisationId();

        // Assert
        organisationIdReturned.Should().Be(organisationId);
    }

    [Fact]
    public void Should_RetrieveProcedureNoteIdAsAnInt_When_UsingNonGenericMethod()
    {
        // Arrange
        var procedureNoteId = 12345;
        var streamId = $"6D9D2DCD-E85B-4EAE-8419-38A94C31D7A3|49AC0617-6A01-453C-A051-5F0709CFF601|{procedureNoteId}";
        var streamIdManager = new ProcedureNoteStreamIdManager(streamId);

        // Act
        var procedureNoteIdReturned = streamIdManager.GetProcedureNoteId();

        // Assert
        procedureNoteIdReturned.Should().Be(procedureNoteId);
    }

    [Fact]
    public void Should_RetrieveProcedureNoteIdAsAnInt_When_UsingGenericMethod()
    {
        // Arrange
        var procedureNoteId = 12345;
        var streamId = $"6D9D2DCD-E85B-4EAE-8419-38A94C31D7A3|49AC0617-6A01-453C-A051-5F0709CFF601|{procedureNoteId}";
        var streamIdManager = new ProcedureNoteStreamIdManager(streamId);

        // Act
        var procedureNoteIdReturned = streamIdManager.GetValue<int>("ProcedureNoteId");

        // Assert
        procedureNoteIdReturned.Should().Be(procedureNoteId);
    }

    [Fact]
    public void Should_ThrowAnException_When_OriginalAndRequestedTypesAreMismatched()
    {
        // Arrange
        var streamIdManager = new StreamIdManagerBuilder()
            .WithTypeAndKey<Guid>("ProcedureNoteId")
            .Build();

        streamIdManager.Load("12345");

        // Act
        var act = () => streamIdManager.GetValue<int>("ProcedureNoteId");

        // Assert
        act.Should()
            .Throw<Exception>()
            .WithMessage("The type requested 'Int32' does not match original type of 'Guid'");
    }

    [Fact]
    public void Should_ThrowAnException_When_SegmentKeyCannotBeFound()
    {
        // Arrange
        var streamIdManager = new StreamIdManagerBuilder().Build();
        var segmentKey = "ProcedureNote";
        streamIdManager.Load("12345");

        // Act
        var act = () => streamIdManager.GetValue<int>(segmentKey);

        // Assert
        act.Should()
            .Throw<Exception>()
            .WithMessage($"The requested segment key '{segmentKey}' cannot be found");
    }

    [Fact]
    public void Should_ThrowAnException_When_TryingToConvertedAGuidToAnInt()
    {
        // Arrange
        var patientId = new Guid("6D9D2DCD-E85B-4EAE-8419-38A94C31D7A3");
        var streamId = $"{patientId}|49AC0617-6A01-453C-A051-5F0709CFF601|12345";
        var streamIdManager = new StreamIdManagerBuilder()
            .WithDelimiter("|")
            .WithTypeAndKey<int>("ProcedureNoteId")
            .Build();
        streamIdManager.Load(streamId);

        // Act
        var act = () => streamIdManager.GetValue<int>("ProcedureNoteId");

        // Assert
        act.Should().Throw<Exception>().WithMessage($"Unable to convert string value '{patientId}' to type Int32");
    }
}