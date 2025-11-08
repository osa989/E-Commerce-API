using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = [];
        private readonly StoreDbContext _dbContext;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType,out object? repository))
            return (IGenericRepository<TEntity, TKey>)repository;
           var NewRepo = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories[EntityType] = NewRepo;
                return NewRepo;
            
        }

        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
    }
}
