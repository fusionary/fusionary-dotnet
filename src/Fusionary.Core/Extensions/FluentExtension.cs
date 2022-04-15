namespace Fusionary.Core.Extensions;

public static class FluentExtension {
    public static T When<T>(this T thisInstance, bool condition, Func<T, T> conditionalAction) {
        return condition ? conditionalAction(thisInstance) : thisInstance;
    }
    
    public static T With<T>(this T thisInstance, Action<T> withAction)
    {
        withAction(thisInstance);
        return thisInstance;
    }
}