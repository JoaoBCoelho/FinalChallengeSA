namespace FinalChallengeSA.Application.DTOs
{
    public sealed record ProductRequest(string Name, string Description, decimal Price);
    public sealed record ProductResponse(Guid Id, string Name, string Description, decimal Price);
}
