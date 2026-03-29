using FinalChallengeSA.Domain.Entities;

namespace FinalChallengeSA.Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyCollection<Product>> GetByNameAsync(string name, CancellationToken ct = default);
    }
}
