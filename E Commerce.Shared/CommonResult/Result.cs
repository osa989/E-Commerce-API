using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    // # If Success and data returned => use this class , like Create , Update, Delete operations
    public class Result
    {
        protected readonly List<Error> _errors = [];
        public bool IsSuccess => _errors.Count ==0;
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors => _errors; // encapsulate the errors list so that it cannot be modified from outside , accessed through this property from outside

        // when using constructor when success 
        protected Result()
        {
            
        }
        // when using constructor when failure
        protected Result( Error error)
        {
          _errors.Add(error);
        }
        // when using constructor when failure with multiple errors
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }

        // but maybe I use factory methods insead of constructors
        public static Result Ok()=> new Result();
        public static Result Fail(Error error) => new Result (error);

        public static Result Fail(List<Error> errors) => new Result (errors);
    }

    // # If Success and data returned => use this class , like Get operations
    public class Result<TValue> : Result  // for success result with value
    {

        private readonly TValue _value;
        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access the value of a failed result.");

        //OK success result with value
        private Result(TValue value) : base()
        {
            _value = value;
        }
        // Fail failure result with error
        private Result(Error error) : base(error)
        {
            _value = default!; // null 
        }
        // Fail failure result with multiple errors
        private Result(List<Error> errors) : base(errors)
        {
            _value = default!;
        }

        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static new Result<TValue> Fail(Error error) => new Result<TValue>(error); // there will be warning here because you alraeady inherited from base class so you can mask this method

        public static new Result<TValue> Fail(List<Error> errors) => new Result<TValue>(errors);


        #region syntax sugar
        // syntax sugar 
        //public static new Result<TValue> Fail(Error error) => new(error); 
        #endregion


    }
    //we made all consturects protected to prevent direct instantiation from outside the class and allow only factory methods to create instances which inherits from the result class
}
