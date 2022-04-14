namespace Fusionary.Data.Models;

/// <summary>
/// Represents filter for paging.
/// </summary>
/// <param name="Page">Page number.</param>
/// <param name="Limit">Result limit.</param>
public record PaginationFilter(int Page, int Limit) { }