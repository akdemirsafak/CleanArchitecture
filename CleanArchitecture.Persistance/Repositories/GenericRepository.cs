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

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        var updatedEntity= _dbSet.Update(entity);
        updatedEntity.State = EntityState.Modified;
    }
}
