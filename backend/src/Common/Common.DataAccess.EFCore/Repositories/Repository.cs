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

        public bool Add(TEntity entity)
        {
            try
            {
                entity.Created();
                Context.Set<TEntity>().Add(entity);
                SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                entity.Created();
                await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
                await SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                foreach (var e in entities)
                {
                    e.Created();
                }
                Context.Set<TEntity>().AddRange(entities);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var e in entities)
                {
                    e.Created();
                }
                await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
                await SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public bool Remove(int id)
        {
            try
            {
                var entity = Context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
                if (entity != null)
                {
                    Context.Set<TEntity>().Remove(entity);
                    SaveChanges();
                    return true;    
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public bool RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().RemoveRange(entities);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                var exist = GetById(entity.Id);
                if (exist == null)
                    return false;
                entity.Modified();
                Context.Set<TEntity>().Update(entity);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                foreach (var e in entities)
                {
                    e.Modified();
                }
                Context.Set<TEntity>().UpdateRange(entities);
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
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
