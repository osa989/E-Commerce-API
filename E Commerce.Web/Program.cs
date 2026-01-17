
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.IdentityData.DataSeed;
using E_Commerce.Persistence.IdentityData.DbContexts;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Services;
using E_Commerce.Services.MappingProfiles;
using E_Commerce.Services_Abstraction;
using E_Commerce.Web.CustomMiddlewares;
using E_Commerce.Web.Extentions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
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
            builder.Services.AddKeyedScoped<IDataIntializer, DataIntializer>("Default");
            builder.Services.AddKeyedScoped<IDataIntializer, IdentityDataIntialize>("Identity");
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddAutoMapper(X=>X.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(typeof(ServiceAssemplyReference).Assembly); // register all profiles in the assembly
            //builder.Services.AddAutoMapper(X=>X.LicenseKey = "",typeof(ProductProfile).Assembly); // allow dependency injection in all profiles 

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<ProductPictureUrlResolver>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(Sp =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });
            builder.Services.AddDbContext<StoreIdentityDbContext>(options=> {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            //builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
            //    .AddEntityFrameworkStores<StoreIdentityDbContext>()
            //    .AddDefaultTokenProviders(); // not best practice because it registers many services that we may not use (All services related to identity )
            builder.Services.AddIdentityCore<ApplicationUser>()// register management only
                .AddRoles<IdentityRole>() // register role management 
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            #endregion

            var app = builder.Build();
            #region DataSeed - apply Migration

            await app.MigrateDatabaseAsync();
            await app.MigrateIdentityDatabaseAsync();
            await app.SeedDatabaseAsync();
            await app.SeedIdentityDatabaseAsync();



            #endregion


            #region Configure the HTTP request pipeline.
            #region way to create middleware 
            //app.Use(async (Context, Next) =>
            //{
            //    try
            //    {
            //        await Next.Invoke();

            //    }
            //    catch (Exception ex)
            //    {
            //        //log exception
            //        Console.WriteLine(ex.Message);
            //        Context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        await Context.Response.WriteAsJsonAsync(new
            //        {
            //            StatusCode = StatusCodes.Status500InternalServerError,
            //            Error = $"UnExpected error occured :{ex.Message}"
            //        });// or you can create a custom error response Class 
            //    }
            //}); 
            #endregion
            app.UseMiddleware<ExceptionHandlerMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // to serve static files from wwwroot folder but in version 9 and above its enabled by default

            app.MapControllers(); 
            #endregion

           await app.RunAsync();
        }
    }
}
