using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithTypeAndBrandSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        // If brandId is provided (not null), filter products where P.BrandId == brandId
        // If typeId is provided (not null), filter products where P.TypeId == typeId
        // If both brandId and typeId are provided, filter products where P.BrandId == brandId AND P.TypeId == typeId
        // If both brandId and typeId are null, return all products without any filtering
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId.Value)
       && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId.Value)
            && (string.IsNullOrEmpty(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescinding(P => P.Price);
                    break;
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescinding(P => P.Name);
                    break;
                default:
                    AddOrderBy(P => P.Id);
                    break;


            }

            
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
    }
}
