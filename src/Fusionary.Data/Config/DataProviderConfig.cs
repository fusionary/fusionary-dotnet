using System.Data.Common;

using SqlKata.Compilers;

namespace Fusionary.Data.Config;

public class DataProviderConfig : IDataProviderConfig {
    public DataProviderConfig(
        ICompilerFactory compilerFactory,
        IConnectionStringBuilder connectionStringBuilder,
        IConnectionFactory connectionFactory
    )
    {
        CompilerFactory = compilerFactory;
        ConnectionStringBuilder = connectionStringBuilder;
        ConnectionFactory = connectionFactory;
    }

    public ICompilerFactory CompilerFactory { get; set; }
    public IConnectionStringBuilder ConnectionStringBuilder { get; set; }
    public IConnectionFactory ConnectionFactory { get; set; }

    public Compiler CreateCompiler()
    {
        return CompilerFactory.CreateCompiler();
    }

    public DbConnection CreateConnection(string connectionString)
    {
        return ConnectionFactory.CreateConnection(connectionString);
    }

    public string GetConnectionString(DatabaseConfig config)
    {
        return ConnectionStringBuilder.GetConnectionString(config);
    }
}
