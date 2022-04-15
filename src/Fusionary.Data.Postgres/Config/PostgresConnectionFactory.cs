using System.Data.Common;

using Fusionary.Data.Config;

using Npgsql;

namespace Fusionary.Data.Postgres.Config;

public class PostgresConnectionFactory : IConnectionFactory {
    public DbConnection CreateConnection(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}
