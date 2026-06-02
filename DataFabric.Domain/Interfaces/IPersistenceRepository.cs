using System.Linq.Expressions;

namespace DataFabric.Domain.Interfaces
{
    public interface IPersistenceRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
