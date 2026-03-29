namespace FinalChallengeSA.Domain.Entities;

public sealed record Order(Guid Id, Guid CustomerId, IReadOnlyCollection<Product> Products, decimal TotalAmount);
