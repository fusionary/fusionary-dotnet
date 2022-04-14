using Microsoft.Extensions.Primitives;

namespace Fusionary.Web.Extensions;

public static class StringValueExtensions {
    public static StringValues ToStringValues(this IEnumerable<string> values) {
        return values.Aggregate(StringValues.Empty, (current, next) => StringValues.Concat(current, next));
    }
}