namespace FinalChallengeSA.Application.DTOs
{
    public sealed record CustomerRequest(string Name, string Email);
    public sealed record CustomerResponse(Guid Id, string Name, string Email);
}
