using Fusionary.Data.Config;

using Npgsql;

namespace Fusionary.Data.Postgres.Config;

public class PostgresConnectionBuilder : IConnectionStringBuilder {

    private readonly Action<NpgsqlConnectionStringBuilder>? connectionOptions;

    public PostgresConnectionBuilder(Action<NpgsqlConnectionStringBuilder>? connectionOptions = null)
    {
        this.connectionOptions = connectionOptions;
    }

    /// <inheritdoc />
    public string GetConnectionString(DatabaseConfig config)
    {
        var builder = CreateNpgsqlConnectionStringBuilder(config);
        connectionOptions?.Invoke(builder);
        return builder.ToString();
    }

    private static NpgsqlConnectionStringBuilder CreateNpgsqlConnectionStringBuilder(DatabaseConfig config) =>
        new () {
            Host = config.Host,
            Port = config.Port,
            Database = config.Database,
            Username = config.Username,
            Password = config.Password,
            Enlist = true,
            SslMode = SslMode.Prefer,
        };
}