using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Error
    {
        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }
//
        public string Code { get;  }
        public string Description { get; }

        public ErrorType Type { get;  }

        //static factory method to create error and making constructor private this another way to set values
        public static Error Failure(string code ="General Failure", string description = "General Failure has occured  ") // this is initial value if user dont provide any value
        {
            return new Error (code , description, ErrorType.Failure);  
        }
        public static Error Validation(string code = "Validation Error", string description = "Validation Error has occured")
        {
            return new Error(code, description, ErrorType.Validation);
        }
        public static Error NotFound(string code = "Not Found", string description = "The requested resource was not found")
        {
            return new Error(code, description, ErrorType.NotFound);
        }
        public static Error Unauthorized( string code = "Unauthorized", string description = "You are not authorized to access this resource")
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }
        public static Error Forbidden(string code = "Forbidden", string description = "You do not have permission to access this resource")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }
        public static Error InvalidCredintials(string code = "Invalid Credintials", string description = "The provided credintials are invalid")
        {
            return new Error(code, description, ErrorType.InvalidCredintials);
        }

    }
}
