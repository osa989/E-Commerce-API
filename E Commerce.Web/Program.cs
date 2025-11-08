
using E_Commerce.Domain.Contract;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Web.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        { 
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Add services to the container 
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataIntializer, DataIntializer>();
            #endregion

            var app = builder.Build();
            #region DataSeed - apply Migration

            await app.MigrateDatabaseAsync();
                await app.SeedDatabaseAsync();



            #endregion


            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.MapControllers(); 
            #endregion

           await app.RunAsync();
        }
    }
}
