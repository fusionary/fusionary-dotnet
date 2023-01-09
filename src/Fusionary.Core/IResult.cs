using System.Runtime.ExceptionServices;

namespace Fusionary.Core;

public interface IResult {
    ExceptionDispatchInfo? Error { get; }

    string? Message { get; }

    object? ProblemDetail { get; }

    ResultStatus Status { get; }

    object? Value { get; }

    bool HasValue { get; }

    bool HasError { get; }

    bool HasProblemDetail { get; }
}
