using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AMS.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        Task<TEntity> GetAsync(int id);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entities);

        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(ICollection<TEntity> entities);

        Task UpdateAsync(TEntity entity);


    }
}
