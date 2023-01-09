using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace Fusionary.Core;

public class Result<T> : IResult {
    public bool HasValue => Value != null;

    public bool HasError => Error != null;

    public bool HasProblemDetail => ProblemDetail != null;

    public ExceptionDispatchInfo? Error { get; set; }

    public string? Message { get; set; }

    public object? ProblemDetail { get; set; }

    public ResultStatus Status { get; set; }

    public T? Value { get; set; }

    object? IResult.Value => Value;

    public static implicit operator Result<T>(T value)
    {
        return Result.Ok(value);
    }

    public static implicit operator T(Result<T> result)
    {
        return result.GetValue();
    }

    public static implicit operator bool(Result<T> result)
    {
        return result.Status == ResultStatus.Ok;
    }

    [return: NotNull]
    public T GetValue()
    {
        return Value == null ? throw new InvalidOperationException("Value is null") : Value;
    }
}
