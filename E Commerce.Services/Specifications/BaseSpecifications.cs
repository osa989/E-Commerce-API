using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = []; // Read only Property get its value by the constructor 
        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExp)
        {
            IncludeExpressions.Add(IncludeExp);
        }
    }
}
