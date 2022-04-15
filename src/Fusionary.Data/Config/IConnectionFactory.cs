using System.Data.Common;

namespace Fusionary.Data.Config;

public interface IConnectionFactory {
    public DbConnection CreateConnection(string connectionString);
}
