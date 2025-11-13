  using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    internal static class SpecificationEvaluater
    {
        //_dbContext.Products.Include(P => P.ProductType).Include(P => P.ProductBrand);  Query to be built
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,
            ISpecifications<TEntity, TKey> specifications)where TEntity : BaseEntity<TKey>
        {
            //dbcontext.Products
            var Query = EntryPoint;
            if (specifications is not null)
            {
                //checked for Criteria
                if (specifications.Criteria is not null )
                {
                    Query = Query.Where(specifications.Criteria);
                }

                // check wether includes are not null and not empty
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
               {
                    #region using foreach
                    //foreach (var includeExp in specifications.IncludeExpressions)
                    //{
                    //    EntryPoint= EntryPoint.Include(includeExp);
                    //} 
                    #endregion
                    Query = specifications.IncludeExpressions
                        .Aggregate(Query, (CurrentQuery, includeExp) => CurrentQuery.Include(includeExp));
                }
            }
                return Query;
        }
    }
}
