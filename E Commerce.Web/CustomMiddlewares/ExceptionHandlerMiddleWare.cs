using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.CustomMiddlewares
{
    public class ExceptionHandlerMiddleWare 
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleWare(RequestDelegate Next) 
        {
            next = Next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            try
            {
                await next.Invoke(httpContext);
                await HandleNotFoundEndpointAsync(httpContext);
            }
            catch (Exception ex)
            {
                // Log the exception 
                logger.LogError(ex, "An unhandled exception occurred.");
                //this will log in the console as well as in the file if configured

                // Return Custom Error Response
                var Problem = new ProblemDetails()
                {
                    Title = "An error occurred while processing your request.",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = ex switch
                    {
                       NotFoundException => StatusCodes.Status404NotFound,
                       _=> StatusCodes.Status500InternalServerError
                    }
                };
                httpContext.Response.StatusCode =  Problem.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(Problem);
            }
        }

        private static async Task HandleNotFoundEndpointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound&&!httpContext.Response.HasStarted)
            {
                var Problem = new ProblemDetails()
                {
                    Title = "Error will processing the http request - endpoint not found ",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"EndPoint {httpContext.Request.Path} Not found ",
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(Problem);
            }
        }
    }
}
