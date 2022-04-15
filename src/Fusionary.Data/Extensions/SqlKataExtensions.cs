using Fusionary.Data.Models;

using SqlKata;

namespace Fusionary.Data.Extensions;

public static class SqlKataExtensions {
    public static Query OrderBy(this Query query, SortDirection direction, params string[] columns) {
        return query.When(direction == SortDirection.Asc, q => q.OrderBy(columns), q => q.OrderByDesc(columns));
    }
}
