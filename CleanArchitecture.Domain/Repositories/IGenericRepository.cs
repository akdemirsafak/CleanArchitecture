using System.Linq.Expressions;

namespace CleanArchitecture.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IList<T>> GetAllAsync();
    IQueryable<T> GetList();
    Task<T> GetByIdAsync(int id);
}
