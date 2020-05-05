using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        // This method was not in the videos, but I thought it would be useful to add.
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault();
        Task<TEntity> FirstOrDefaultAsync();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Paging(IQueryable<TEntity> entities, Paging paging);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Remove(int id);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
