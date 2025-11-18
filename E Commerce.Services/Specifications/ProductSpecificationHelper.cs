using E_Commerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    internal class ProductSpecificationHelper
    {
        public static Expression<Func<Product, bool>>? GetProductCriteria(ProductQueryParams queryParams)
        {
            return P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId.Value)
       && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId.Value)
            && (string.IsNullOrEmpty(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower()));
        }
    }
}
