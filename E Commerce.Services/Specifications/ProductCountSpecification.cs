using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId.Value)
       && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId.Value)
            && (string.IsNullOrEmpty(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower())))
        {

        }
    }  
}
