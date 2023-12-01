namespace YEE.Identity.API.Models
{
    public class ApiResult<T>
    {
        private ApiResult(bool succeeded, T result, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Result = result;
            Errors = errors;
        }

        public bool Succeeded { get; set; }

        public T Result { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public static ApiResult<T> Success(T result)
        {
            return new ApiResult<T>(true, result, new List<string>());
        }

        public static ApiResult<T> Failure(IEnumerable<string> errors)
        {
            return new ApiResult<T>(false, default, errors);
        }
        public static ApiResult<T> Failure(string error)
        {
            return new ApiResult<T>(false, default, new List<string>() { error });
        }
    }
}
