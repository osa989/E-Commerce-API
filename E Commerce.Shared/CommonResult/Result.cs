using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Result
    {
        private readonly List<Error> _errors = [];
        public bool IsSuccess => _errors.Count ==0;
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors => _errors;

        // when using constructor when success 
        private Result()
        {
            
        }
        // when using constructor when failure
        private Result( Error error)
        {
          _errors.Add(error);
        }
        // when using constructor when failure with multiple errors
        private Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }

        // but maybe I use factory methods insead of constructors
        public static Result Ok()=> new Result();
        public static Result Fail(Error error) => new Result (error);

        public static Result Fail(List<Error> errors) => new Result (errors);
    }
}
