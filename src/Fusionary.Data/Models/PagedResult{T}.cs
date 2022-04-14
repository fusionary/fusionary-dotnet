namespace Fusionary.Data.Models;

/// <summary>
/// Represents a paged result collection.
/// </summary>
/// <typeparam name="T">Result type.</typeparam>
public record PagedResult<T>(IReadOnlyList<T> Items, int Page, int Limit, int Total) {
    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages {
        get {
            if (Limit < 1) {
                return 0;
            }

            var div = (float)Total / Limit;

            return (int)Math.Ceiling(div);
        }
    }
}