using System.Data.Common;

using Fusionary.Data.Config;

using JetBrains.Annotations;

using Microsoft.Extensions.Options;

using SqlKata;
using SqlKata.Execution;

namespace Fusionary.Data;

[UsedImplicitly]
public abstract class DbServiceBase : IDbService {
    private readonly IOptions<DatabaseConfig> config;
    private readonly IDataProviderConfig dataProviderConfig;

    protected DbServiceBase(IOptions<DatabaseConfig> config, IDataProviderConfig dataProviderConfig)
    {
        this.config = config;
        this.dataProviderConfig = dataProviderConfig;
    }

    protected string ConnectionString => dataProviderConfig.GetConnectionString(config.Value);

    protected async Task<T> ExecuteAsync<T>(Func<DbConnection, Task<T>> executeFunc, CancellationToken cancellationToken = new()) {
        return await Task.Run(async () => {
                await using var connection = dataProviderConfig.CreateConnection(ConnectionString);

                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                return await executeFunc(connection).ConfigureAwait(false);
            },
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a SQL Kata style QueryFactory operation
    /// <seea href="https://sqlkata.com/docs/" />
    /// </summary>
    protected async Task<T> ExecuteQueryFactoryAsync<T>(Func<QueryFactory, Task<T>> executeFunc, CancellationToken cancellationToken = new()) {
        return await Task.Run(async () => {
                await using var connection = dataProviderConfig.CreateConnection(ConnectionString);

                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                using var db = new QueryFactory(connection, dataProviderConfig.CreateCompiler()) {
                    Logger = QueryFactoryLogger(),
                };

                return await executeFunc(db).ConfigureAwait(false);
            },
            cancellationToken).ConfigureAwait(false);
    }

    protected virtual Action<SqlResult> QueryFactoryLogger() =>
        compiled => {
            Console.WriteLine(compiled.ToString());
        };
}
