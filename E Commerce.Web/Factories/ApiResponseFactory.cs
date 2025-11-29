using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext) {
        
            
                #region explaining of model state errors
                // Product 

                //[Required] // Model state01
                //[MaxLength = 20]  //Model state02 
                // Name
                //[Required]  // Model state03
                // Id 
                //[Required] // Model state04
                //[Range (1,1000)] // Model state05
                // Price
                //[Range (1,100)] // Model state06
                // Quantity

                // key : Model property Name
                // value : array of error messages 
                #endregion

                var errors = actionContext.ModelState.Where(X => X.Value.Errors.Count > 0)
                .ToDictionary(X => X.Key,
                X => X.Value.Errors.Select(X => X.ErrorMessage).ToArray());

                var Problem = new ProblemDetails()
                {
                    Title = " Validation Error",
                    Detail = "One or more vlalidation errors occurred",
                    Status = StatusCodes.Status400BadRequest,
                    Extensions =
                        {
                            { "Errors", errors }
                        }
                };
                return new BadRequestObjectResult(Problem);
            }
        
    }
}
