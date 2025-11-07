using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    internal class DataIntializer : IDataIntializer
    {
        private readonly StoreDbContext _dbContext;

        public DataIntializer(StoreDbContext dbContext)
        {
                _dbContext = dbContext;
        }

        public void Intialize()
        {
            try
            {
                var HasProducts = _dbContext.Products.Any();
                var HasBrands = _dbContext.ProductBrands.Any();
                var HasTypes = _dbContext.ProductTypes.Any();

                if (HasBrands && HasTypes && HasProducts) return;

                if (!HasBrands)
                {
                    SeedDataFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }

                if (!HasTypes)
                {
                    SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);
                }

                _dbContext.SaveChanges(); //already made but we wrote it here to make sure that the brands and types are saved before products

                if (!HasProducts)
                {
                    SeedDataFromJson<Product, int>("products.json", _dbContext.Products);
                }
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data seed is failed {ex}");
            }
        }
        private void SeedDataFromJson<T,TKey>(string FileName,DbSet<T> dbset)where T : BaseEntity<TKey>
        {
            var FilePath = @"..\E Commerce.Persistence\Data\DataSeed\JSONFiles" + FileName;

            if (!File.Exists(FilePath)) throw new FileNotFoundException($"Json File Not Found in Path : {FilePath}");
            try
            {
                using var dataStreams = File.OpenRead(FilePath);

                var data = JsonSerializer.Deserialize<List<T>>(dataStreams, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data is not null)
                    dbset.AddRange(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Reading Json File : {ex}");
                return;
            }

        }
    }
}
