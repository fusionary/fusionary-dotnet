using Dapper;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Fusionary.Data.Postgres;

[UsedImplicitly]
public static class PostgresExtensions {
    public static void UsePostgres(this IServiceCollection services, Action<PostgresOptions>? configure = null)
    {
        var options = new PostgresOptions();
        
        configure?.Invoke(options);
        
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        services.AddSingleton(PostgresProviderConfig.Create(options));
    }
}
