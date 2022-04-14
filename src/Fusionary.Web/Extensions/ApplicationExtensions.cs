using Microsoft.AspNetCore.Builder;

namespace Fusionary.Web.Extensions;

/// <summary>
/// Extension methods for <see cref="IApplicationBuilder" />.
/// </summary>
public static class ApplicationExtensions {
    /// <summary>
    /// Executes the specified action if the specified <paramref name="condition" /> is <c>true</c> which can be used to
    /// conditionally add to the app.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <param name="condition">If set to <c>true</c> the action is executed.</param>
    /// <param name="action">The action used to add to the silo builder.</param>
    /// <returns>The same silo builder.</returns>
    public static IApplicationBuilder UseIf(
        this IApplicationBuilder app,
        bool condition,
        Func<IApplicationBuilder, IApplicationBuilder> action
    ) {
        ArgumentNullException.ThrowIfNull(app);
        ArgumentNullException.ThrowIfNull(action);

        if (condition) {
            app = action(app);
        }

        return app;
    }

    /// <summary>
    /// Executes the specified action if the specified <paramref name="condition" /> is <c>true</c> which can be used to
    /// conditionally add to the app.
    /// </summary>
    /// <param name="app">The app.</param>
    /// <param name="condition">If <c>true</c> is returned the action is executed.</param>
    /// <param name="action">The action used to add to the silo builder.</param>
    /// <returns>The same silo builder.</returns>
    public static IApplicationBuilder UseIf(
        this IApplicationBuilder app,
        Func<IApplicationBuilder, bool> condition,
        Func<IApplicationBuilder, IApplicationBuilder> action
    ) {
        ArgumentNullException.ThrowIfNull(app);
        ArgumentNullException.ThrowIfNull(condition);
        ArgumentNullException.ThrowIfNull(action);

        if (condition(app)) {
            app = action(app);
        }

        return app;
    }
}