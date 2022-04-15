using Dapper;

using Fusionary.Data.Config;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace Fusionary.Data.Postgres;

[UsedImplicitly]
public static class PostgresExtensions {
    public static IDataProviderConfig UsePostgres(this IServiceCollection services)
    {
        return UsePostgres(services, _ => { });
    }

    public static IDataProviderConfig UsePostgres(this IServiceCollection services, Action<PostgresOptions> setupAction)
    {
        var options = new PostgresOptions();
        
        setupAction(options);
        
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        var provider = PostgresProviderConfig.Create(options);
        
        services.AddSingleton(provider);

        return provider;
    }
}
