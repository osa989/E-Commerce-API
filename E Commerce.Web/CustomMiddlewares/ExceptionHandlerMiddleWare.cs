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

                if(httpContext.Response.StatusCode== StatusCodes.Status404NotFound)
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
            catch (Exception ex)
            {
                // Log the exception 
                logger.LogError(ex, "An unhandled exception occurred.");
                //this will log in the console as well as in the file if configured
                // Return Custom Error Response
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var Problem = new ProblemDetails()
                {
                    Title = "An error occurred while processing your request.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(Problem);
            }
        }
    }
}
