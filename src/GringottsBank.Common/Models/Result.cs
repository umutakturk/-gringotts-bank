namespace GringottsBank.Common.Models
{
    public class Result<TData>
    {
        public Result(TData data, ErrorResult error)
        {
            Data = data;
            Error = error;
        }

        public bool Succeeded => Error == null;
        public TData Data { get; }
        public ErrorResult Error { get; }
    }

    public static class Result
    {
        public static Result<TData> Failure<TData>(string[] errorMessages)
            => new(default, new ErrorResult(errorMessages));

        public static Result<TData> Success<TData>(TData value = default)
            => new(value, null);
    }
}
