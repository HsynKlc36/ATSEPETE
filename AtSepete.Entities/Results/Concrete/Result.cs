namespace AtSepete.Results.Concrete;

    public class Result : IResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public Result()
        {
            IsSuccess = false;
            Message = string.Empty;
        }
        public Result(bool isSuccess) : this() => IsSuccess = isSuccess;
        public Result(bool isSuccess, string message) : this(isSuccess) => Message = message;
    }

