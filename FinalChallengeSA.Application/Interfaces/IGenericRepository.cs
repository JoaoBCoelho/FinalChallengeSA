namespace FinalChallengeSA.Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyCollection<TEntity>> GetByNameAsync(string name, CancellationToken ct = default);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteAsync(TEntity entity, CancellationToken ct = default);
        Task<int> CountAsync(CancellationToken ct = default);
    }
}
