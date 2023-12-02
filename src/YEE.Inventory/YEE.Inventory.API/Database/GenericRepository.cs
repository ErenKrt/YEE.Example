using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace YEE.Inventory.API.Database
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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        protected virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsNoTracking();
        }
        public IQueryable<TEntity> GetAll()
        {
            return GetAllIncluding(Array.Empty<Expression<Func<TEntity, object>>>());
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetQueryable();

            foreach (var includeProperty in propertySelectors)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await GetAll().FirstOrDefaultAsync(predicate);

        public virtual TEntity FirstOrDefault(int id) => GetAll().FirstOrDefault(x => x.ID == id);

        public TEntity Insert(TEntity entity)
        {
            var add = _dbSet.Add(entity);
            _context.SaveChanges();
            return add.Entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var add = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return add.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private TEntity GetFromChangeTrackerOrNull(int id) => _context.ChangeTracker.Entries<TEntity>()
                .FirstOrDefault(entry => entry.Entity.ID == id)?.Entity;

        public void Delete(int id)
        {
            TEntity tentity = GetFromChangeTrackerOrNull(id);
            if (tentity != null)
            {
                Delete(tentity);
                return;
            }
            tentity = FirstOrDefault(id);
            if (tentity != null)
            {
                Delete(tentity);
                return;
            }
        }

        public async Task DeleteAsync(int id)
        {
            TEntity tentity = GetFromChangeTrackerOrNull(id);
            if (tentity != null)
            {
                await DeleteAsync(tentity);
                return;
            }
            tentity = FirstOrDefault(id);
            if (tentity != null)
            {
                await DeleteAsync(tentity);
                return;
            }
        }

        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

    }
}
