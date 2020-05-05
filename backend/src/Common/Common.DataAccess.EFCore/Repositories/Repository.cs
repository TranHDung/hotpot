using Common.DTO;
using Common.Entities;
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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DataContext Context;

        public Repository(DataContext context)
        {
            Context = context;
        }

        public TEntity GetById(int id)
        {
            // Here we are working with a DbContext, not PlutoContext. So we don't have DbSets 
            // such as Courses or Authors, and we need to use the generic Set() method to access them.
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity FirstOrDefault()
        {
            return Context.Set<TEntity>().FirstOrDefault();
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> Paging(IQueryable<TEntity> entities, Paging paging)
        {
            return entities.Skip(paging.Skip).Take(paging.Top);
        }

        public void Add(TEntity entity)
        {
            entity.Created();
            Context.Set<TEntity>().Add(entity);
            SaveChanges();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Created();
            await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var e in entities)
            {
                e.Created();
            }
            Context.Set<TEntity>().AddRange(entities);
            SaveChanges();
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var e in entities)
            {
                e.Created();
            }
            await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public void Remove(int id)
        {
            var entity = Context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            if (entity != null) 
            {
                Context.Set<TEntity>().Remove(entity);
                SaveChanges();
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            SaveChanges();
        }

        public void Update(TEntity entity)
        {
            entity.Modified();
            Context.Set<TEntity>().Update(entity);
            SaveChanges();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var e in entities)
            {
                e.Modified();
            }
            Context.Set<TEntity>().UpdateRange(entities);
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
