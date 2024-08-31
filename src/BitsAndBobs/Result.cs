namespace BitsAndBobs;

public abstract class Result<T>
{
    public abstract bool IsSuccess { get; }
    public abstract T Value { get; }
    public abstract string Error { get; }

    public static Result<T> Success(T value) => new SuccessResult<T>(value);
    public static Result<T> Failure(string error) => new FailureResult<T>(error);

    public abstract Result<U> Bind<U>(Func<T, Result<U>> func);

    public Result<U> Map<U>(Func<T, U> func) => Bind(x => Result<U>.Success(func(x)));

    public T GetValueOrDefault(T defaultValue)
    {
        return IsSuccess ? Value : defaultValue;
    }
}

public class SuccessResult<T> : Result<T>
{
    public override bool IsSuccess => true;
    public override T Value { get; }
    public override string Error => string.Empty;

    public SuccessResult(T value)
    {
        Value = value;
    }

    public override Result<U> Bind<U>(Func<T, Result<U>> func)
    {
        return func(Value);
    }
}

public class FailureResult<T> : Result<T>
{
    public override bool IsSuccess => false;
    public override T Value => throw new InvalidOperationException("No value for failure result");
    public override string Error { get; }

    public FailureResult(string error)
    {
        Error = error;
    }

    public override Result<U> Bind<U>(Func<T, Result<U>> func)
    {
        return Result<U>.Failure(Error);
    }
}
