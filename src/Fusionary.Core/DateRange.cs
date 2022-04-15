namespace Fusionary.Core;

public readonly struct DateRange {
    public DateRange(DateTimeOffset start, DateTimeOffset end) {
        Start = start;
        End = end;
    }

    public DateTimeOffset Start { get; }

    public DateTimeOffset End { get; }

    /// <summary>
    /// Returns <c>true</c> if the specified value is within the current date range
    /// </summary>
    public bool Contains(DateTimeOffset value) {
        return Start <= value && value <= End;
    }

    /// <summary>
    /// Gets a collection of the dates in the date range.
    /// </summary>
    public DateTimeOffset[] Dates {
        get {
            var startDate = Start;

            return Enumerable.Range(0, Days)
                .Select(offset => startDate.AddDays(offset))
                .ToArray();
        }
    }

    /// <summary>
    /// Gets the number of whole days in the date range.
    /// </summary>
    public int Days => (End - Start).Days + 1;
}
