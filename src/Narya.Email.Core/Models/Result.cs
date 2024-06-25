namespace Narya.Email.Core.Models;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public ICollection<string> Errors { get; } = new List<string>();

    protected Result(bool isSuccess, ICollection<string> errors)
    {
        if (isSuccess && errors.Any())
            throw new InvalidOperationException();
        if (!isSuccess && errors.Any() is false)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        foreach (var error in errors) Errors.Add(error);
    }

    public static Result Success() => new Result(true, new List<string>());
    public static Result Failure(ICollection<string> errors) => new Result(false, errors);
    public static Result Failure(string error) => new Result(false, new List<string> { error });
}

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(T value, bool isSuccess, ICollection<string> errors)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true, new List<string>());
    public new static Result<T> Failure(ICollection<string> errors) => new Result<T>(default(T)!, false, errors);
    public new static Result<T> Failure(string error) => new Result<T>(default(T)!, false, new List<string> { error });
}