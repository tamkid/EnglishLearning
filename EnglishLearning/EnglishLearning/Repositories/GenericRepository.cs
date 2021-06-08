using EnglishLearning.EFs;
using EnglishLearning.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnglishLearning.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal EnglishDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(EnglishDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null
                                        , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
                                        , string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }
    }
}
