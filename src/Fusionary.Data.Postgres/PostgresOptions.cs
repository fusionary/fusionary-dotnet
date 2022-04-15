using Npgsql;

namespace Fusionary.Data.Postgres;

public class PostgresOptions {
    public Action<NpgsqlConnectionStringBuilder> ConnectionOptions { get; set; } = _ => { };
}
