using AutoMapper;
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Services.Specifications;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProductService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            // reutrns Product Brands from database
            //I need to map it to BrandDTOs(Automatic mapping)
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);


        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var Spec = new ProductWithTypeAndBrandSpecification();
            var Products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<ProductDTO>>(Products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {

            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
             
            var product =await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            return _mapper.Map<ProductDTO?>(product);
        }
    }
}
