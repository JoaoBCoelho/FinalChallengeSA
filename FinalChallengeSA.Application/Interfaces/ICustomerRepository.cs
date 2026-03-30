using FinalChallengeSA.Domain.Entities;

namespace FinalChallengeSA.Application.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IReadOnlyCollection<Customer>> GetByNameAsync(string name, CancellationToken ct = default);
        Task<bool> HasOrdersAsync(Guid customerId, CancellationToken ct = default);
    }
}
