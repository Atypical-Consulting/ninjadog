namespace DemoLibrary.Contracts.Responses;

public class ValidationFailureResponse
{
    // TODO: The "set" should be "init". Check compatibility with .NET Standard 2.0
    public List<string> Errors { get; set; } = new();
}