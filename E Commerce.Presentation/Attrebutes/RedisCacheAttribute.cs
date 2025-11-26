using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attrebutes
{
    internal class RedisCacheAttribute : ActionFilterAttribute 
    {
        private readonly int durationInMin;

        public RedisCacheAttribute(int DurationInMin =5 )
        {
            durationInMin = DurationInMin;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get Cache Service From DI container
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheKey = CreateCacheKey(context.HttpContext.Request);

            
            // Check if Cache data Exist 
            var CacheVlue =await CacheService.GetAsync(CacheKey);
            if(CacheVlue is not null)
            {
                //return cached data 
                context.Result = new ContentResult()
                {
                    Content = CacheVlue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return; // don't execute the endpoint
            }
            var ExecutedContext = await next.Invoke(); // execute the endpoint
            if(ExecutedContext.Result is OkObjectResult result)
            {
                //store the result in cache 
                await CacheService.SetAsync(CacheKey, result.Value!, TimeSpan.FromMinutes(durationInMin));
            }


            //if exists , return cached data and skip executing of the endpoint
            // if not exits, execute the endpoint and store the result in Cache if the response was 200 Ok Response 






        }
        //possiblie filterations
        // /api/Products
        // /api/Products?brandId=2
        // /api/Products?typeId=1
        // /api/Products?brandId=2&typeId=1
        // /api/Products?=typeId=1&brandId=2
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path); // /api/Products
            foreach (var item in request.Query.OrderBy(X=>X.Key)) // /api/Products|brandId-2|typeId=1
            {
                Key.Append($"|{item.Key}-{item.Value}");
            }
            return Key.ToString();
        }
    }
}
 