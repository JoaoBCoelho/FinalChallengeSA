using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using FinalChallengeSA.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinalChallengeSA.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order entity, CancellationToken ct = default)
        {
            await _context.Orders.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            return await _context.Orders.CountAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var Order = await GetByIdAsync(id, ct);
            if (Order is null)
            {
                return;
            }

            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Products)
                .ToListAsync(ct);
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(f => f.Id == id, ct);
        }

        public async Task UpdateAsync(Order entity, CancellationToken ct = default)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct = default)
        {
            return await _context.Orders
                .Include(o => o.Products)
                .Where(f => f.CustomerId == customerId)
                .ToListAsync(ct);
        }
    }
}
