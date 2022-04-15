using System.Runtime.ExceptionServices;

namespace Fusionary.Core;

public static class Result {
    public static Result<T> Fail<T>(Exception ex, string? message = default) {
        return new Result<T> {
            Status = ResultStatus.Error,
            Message = message ?? ex.Message,
            Error = ExceptionDispatchInfo.Capture(ex),
        };
    }

    public static Result<T> Fail<T>(string message, object? problemDetail = null) {
        return new Result<T> {
            Status = ResultStatus.Error,
            Message = message,
            ProblemDetail = problemDetail,
        };
    }

    public static Result<T> From<T>(IResult other) {
        return new Result<T> {
            Status = other.Status,
            Message = other.Message,
            Error = other.Error,
            ProblemDetail = other.ProblemDetail,
        };
    }

    public static Result<T> Maybe<T>(T? value) {
        return EqualityComparer<T>.Default.Equals(value) ? NotFound<T>() : Ok<T>(value!);
    }

    public static Result<T> NotFound<T>(string? message = default) {
        return new Result<T> {
            Status = ResultStatus.NotFound,
            Message = message ?? "Not found",
        };
    }

    public static Result<T> Ok<T>(T value, string? message = default) {
        return new Result<T> {
            Status = ResultStatus.Ok,
            Value = value,
            Message = message ?? "Ok",
        };
    }

    internal static Result<T> PermissionDenied<T>(string? message = default) {
        return new Result<T> {
            Status = ResultStatus.PermissionDenied,
            Message = message ?? "Permission Denied",
        };
    }
    
    internal static Result<T> Cancelled<T>(string? message = default) {
        return new Result<T> {
            Status = ResultStatus.Cancelled,
            Message = message ?? "Operation was cancelled",
        };
    }
}
