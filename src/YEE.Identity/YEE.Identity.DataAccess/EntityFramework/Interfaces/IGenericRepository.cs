using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Core.Entities;

namespace YEE.Identity.DataAccess.EntityFramework.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        void Delete(int id);
        void Delete(TEntity entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(TEntity entity);
        TEntity FirstOrDefault(int id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
        Task<List<TEntity>> GetAllListAsync();
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<long> LongCountAsync();
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
