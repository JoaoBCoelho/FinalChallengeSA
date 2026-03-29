namespace FinalChallengeSA.Application.DTOs
{
    public sealed record OrderRequest(Guid CustomerId, IReadOnlyList<Guid> ProductIds);
    public sealed record OrderResponse(Guid Id, Guid CustomerId, IReadOnlyCollection<OrderProductResponse> Products, decimal TotalAmount);
    public sealed record OrderProductResponse(Guid Id, string Name, string Description, decimal Price);
}