using FinalChallengeSA.Domain.Entities;

namespace FinalChallengeSA.Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IReadOnlyCollection<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct = default);
    }
}
