using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using FinalChallengeSA.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinalChallengeSA.Infra.Data.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer entity, CancellationToken ct = default)
        {
            await _context.Customers.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            return await _context.Customers.CountAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var customer = await GetByIdAsync(id, ct);
            if (customer is null)
            {
                return;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<Customer>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Customers.FindAsync([id], ct);
        }

        public async Task<IReadOnlyCollection<Customer>> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(c => c.Name.ToUpper() == name.ToUpper())
                .ToListAsync(ct);
        }

        public async Task<bool> HasOrdersAsync(Guid customerId, CancellationToken ct = default)
        {
            return await _context.Orders.AnyAsync(a => a.CustomerId == customerId, ct);
        }

        public async Task UpdateAsync(Customer entity, CancellationToken ct = default)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
