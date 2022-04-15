namespace Fusionary.Core.Extensions;

public static class CollectionExtensions {
    public static TResult[] SelectToArray<TInput, TResult>(this IEnumerable<TInput> collection, Func<TInput, TResult> selector) {
        return collection.Select(selector).ToArray();
    }

    public static List<TResult> SelectToList<TInput, TResult>(this IEnumerable<TInput> collection, Func<TInput, TResult> selector) {
        return collection.Select(selector).ToList();
    }
}