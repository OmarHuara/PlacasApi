using Elastic.Apm.Api;

namespace PlacasAPI.Utils
{
    public class ValueResult<T> where T : class
    {
        #region constructors
        private ValueResult(Result result, IEnumerable<ResultError> errors)
        {
            Result = result;
            Value = null;
            Errors = errors;
        }
        public ValueResult(Result result, T value)
        {
            Result = result;
            Value = value;
        }
        #endregion

        #region Public methods
        public static ValueResult<T> Success(T value)
            => new ValueResult<T>(Result.Success, value);
        public static ValueResult<T> Fail(string errorMessage)
            => Fail(new List<ResultError>() { new ResultError(errorMessage) });
        public static ValueResult<T> Fail(ResultError error) 
            => Fail(new List<ResultError>() { error });
        public static ValueResult<T> Fail(IEnumerable<ResultError> errors)
            => new ValueResult<T>(Result.Fail, errors);
        #endregion

        #region Public support methods
        public static implicit operator bool(ValueResult<T> valueResult)
        {
            return valueResult.Result.Equals(Result.Success);
        }
        public bool IsSuccess()
        {
            return Result.Equals(Result.Success);
        }
        #endregion

        #region propriedades
        public Result Result { get; private set; }
        public T? Value { get; private set; }
        public IEnumerable<ResultError>? Errors { get; private set; }
        #endregion
    }

    public class ResultError
    {
        public ResultError(string message)
        {
            Code = "__Error__";
            Message = message;
        }

        public ResultError(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; } = null!;
        public string Message { get; set; } = null!;
    }

    public enum Result
    {
        Success = 0,
        Fail = 1
    }
}
