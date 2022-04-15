using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace Fusionary.Core;

public class Result<T> : IResult {
    public ExceptionDispatchInfo? Error { get; set; }
    public bool HasValue => Value != null;

    public string? Message { get; set; }

    public object? ProblemDetail { get; set; }
    public ResultStatus Status { get; set; }

    public T? Value { get; set; }


    [return: NotNull]
    public T GetValue() {
        return Value == null ? throw new InvalidOperationException("Value is null") : Value;
    }

    public static implicit operator Result<T>(T value) {
        return Result.Ok(value);
    }

    public static implicit operator T(Result<T> result) {
        return result.GetValue();
    }
}
