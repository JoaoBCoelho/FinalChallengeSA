using FinalChallengeSA.Domain.Entities;

namespace FinalChallengeSA.Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetByNameAsync(string name, CancellationToken ct = default);
        Task<bool> IsInAnyOrderAsync(Guid productId, CancellationToken ct = default);
    }
}
