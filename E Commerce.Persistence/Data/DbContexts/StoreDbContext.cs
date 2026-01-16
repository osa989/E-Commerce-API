using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DbContexts
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // use it if the configurations and dbcontext in the same project 
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly); // otherwise use this and it will get the rest congiguratoin

    }
            #region DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        #endregion
}
}
