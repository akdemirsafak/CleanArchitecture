using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    public async Task CreateAsync(T entity)
    {
        var addedEntity=await _dbSet.AddAsync(entity);
        addedEntity.State = EntityState.Added;
    }

    public async Task DeleteAsync(T entity)
    {
       var deletedEntity=_dbSet.Remove(entity);
        deletedEntity.State= EntityState.Deleted;
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<T> GetList()
    {
        //return (filter == null ?
        //            _dbSet.Skip((page-1)*pageSize).Take(pageSize) :
        //            _dbSet.Where(filter).Skip((page-1)*pageSize).Take(pageSize)).AsQueryable();

        return _dbSet.AsNoTracking();
    }

    public async Task UpdateAsync(T entity)
    {
        var updatedEntity= _dbSet.Update(entity);
        updatedEntity.State = EntityState.Modified;
    }
}
