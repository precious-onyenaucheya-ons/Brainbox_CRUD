using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Brainbox.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(int id, T entity);
    }
}
