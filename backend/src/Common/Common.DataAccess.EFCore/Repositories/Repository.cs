using Common.DTO;
using Common.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DataContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            // Here we are working with a DbContext, not PlutoContext. So we don't have DbSets 
            // such as Courses or Authors, and we need to use the generic Set() method to access them.
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> Sort(IQueryable<TEntity> entities, Sorting sorting)
        {
            if (sorting != null) 
            {
                if (sorting.sortDerection == "asc")
                {
                    entities = entities.OrderBy(p => typeof(TEntity).GetProperty(sorting.columnName).GetValue(p, null));
                }

                if (sorting.sortDerection == "desc")
                {
                    entities = entities.OrderByDescending(p => typeof(TEntity).GetProperty(sorting.columnName).GetValue(p, null));
                }
            }

            return entities;
        }

        public IQueryable<TEntity> Paging(IQueryable<TEntity> entities, Paging paging)
        {
            return entities.Skip(paging.Skip).Take(paging.Top);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            SaveChanges();
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            SaveChanges();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            SaveChanges();
        }


        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
