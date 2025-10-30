namespace EShop.Catalog.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(T entity);
    Task UpdateAsync();
    Task DeleteAsync(T entity);
}