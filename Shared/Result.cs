namespace entity_framework.Shared
{
    public class Result<T>
    {
        public bool isSuccess { get; }
        public T Value { get; }
        public string ErrorMessage { get; }

        private Result(bool isSuccess, T value, string errorMessage)
        {
            this.isSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Ok(T value) => new Result<T>(true, value, default);
        public static Result<T> Failure(string message) => new Result<T>(false, default, message);
    }
}
