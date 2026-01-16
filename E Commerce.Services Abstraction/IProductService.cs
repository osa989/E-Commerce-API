using E_Commerce.Shared;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction
{
    public interface IProductService
    {
        // Get All Products Return IEnumerable Of Products Data Which Will be
        // {Id , Name, Description , PictureUrl , Price , ProductBrand, ProductType}

        // Get Product By Id Return Product Data Which Will be
        // {Id , Name, Description , PictureUrl , Price , ProductBrand, ProductType}

        // Get All Brands Return IEnumerable Of Brands Data Which Will be {Id , Name}

        // Get All Types Return IEnumerable Of Types Data Which Will be {Id , Name}

        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<Result<ProductDTO>> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}
