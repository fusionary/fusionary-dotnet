using System.Runtime.ExceptionServices;

namespace Fusionary.Core;

public interface IResult {
    ExceptionDispatchInfo? Error { get; set; }
    
    string? Message { get; set; }

    object? ProblemDetail { get; set; }

    ResultStatus Status { get; set; }
}