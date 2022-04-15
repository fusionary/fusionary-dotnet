namespace Fusionary.Core.Extensions;

public static class WhenExtension {
    public static T When<T>(this T thisInstance, bool condition, Func<T, T> conditionalAction) {
        return condition ? conditionalAction(thisInstance) : thisInstance;
    }
}