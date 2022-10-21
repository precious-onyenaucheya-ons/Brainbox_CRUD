using Brainbox.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Brainbox.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
    {
            private readonly BrainboxDbContext _context;
            public GenericRepository(BrainboxDbContext context)
            {
                _context = context;
            }
            public async Task AddAsync(T entity)
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            public async Task DeleteAsync(int id)
            {
                var entity = await _context.Set<T>().FirstOrDefaultAsync(v => v.Id == id);
                EntityEntry entityEntry = _context.Entry<T>(entity);
                entityEntry.State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            public IQueryable<T> GetAll() =>   _context.Set<T>();
            public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
            {
                IQueryable<T> query = _context.Set<T>();
                query = includes.Aggregate(query, (current, include) => current.Include(include));
                return await query.ToListAsync();
            }
            public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(v => v.Id == id);

            public async Task UpdateAsync(int id, T entity)
            {
                EntityEntry entityEntry = _context.Entry<T>(entity);
                entity.Id = id;
                _context.Attach(entity);
                entityEntry.State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        
    }
}
