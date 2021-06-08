using EnglishLearning.EFs;
using EnglishLearning.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLearning.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly EnglishDbContext _context;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(EnglishDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(GenericRepository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));

            return _repositories[type];
        }

        public async Task<int> SaveChange()
        {
            return await _context.SaveChangesAsync();
        }

        #region Disposed

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
