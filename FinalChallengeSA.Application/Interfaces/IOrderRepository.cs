using FinalChallengeSA.Domain.Entities;

namespace FinalChallengeSA.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyCollection<Order>> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken ct = default);
    }
}
