using Fusionary.Data.Config;
using Fusionary.Data.Postgres.Config;

namespace Fusionary.Data.Postgres; 

public static class PostgresProviderConfig {
    public static IDataProviderConfig Create(PostgresOptions options) =>
        new DataProviderConfig(
            new PostgresCompilerFactory(),
            new PostgresConnectionBuilder(options.ConnectionOptions),
            new PostgresConnectionFactory()
        );
}