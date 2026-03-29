namespace FinalChallengeSA.Domain.Entities;

public sealed class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public List<Product> Products { get; private set; } = new();
    public decimal TotalAmount { get; private set; }

    // Construtor sem parâmetros para EF Core
    private Order() { }

    public Order(Guid customerId, IEnumerable<Product> products)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Products = [.. products];
        TotalAmount = Products.Sum(p => p.Price);
    }
}
