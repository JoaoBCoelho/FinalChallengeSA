using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using FinalChallengeSA.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinalChallengeSA.Infra.Data.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product entity, CancellationToken ct = default)
        {
            await _context.Products.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            return await _context.Products.CountAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var product = await GetByIdAsync(id, ct);
            if (product is null)
            {
                return;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Products.FindAsync([id], ct);
        }

        public async Task<Product?> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.ToUpper() == name.ToUpper(), ct);
        }

        public async Task<bool> IsInAnyOrderAsync(Guid productId, CancellationToken ct = default)
        {
            return await _context.Orders.AnyAsync(a => a.Products.Any(product=> product.Id == productId), ct);
        }

        public async Task UpdateAsync(Product entity, CancellationToken ct = default)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
