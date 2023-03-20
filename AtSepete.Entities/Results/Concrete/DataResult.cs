namespace AtSepete.Results.Concrete;

    public class DataResult<T> : Result, IDataResult<T> where T : class
    {
        public T Data { get; }
    
        public DataResult(T data, bool isSuccess) : base(isSuccess) => Data = data;
        public DataResult(T data, bool isSuccess, string message) : base(isSuccess, message) => Data = data;

     
    }

