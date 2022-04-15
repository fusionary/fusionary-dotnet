using System.Data;

using Dapper;

using JetBrains.Annotations;

namespace Fusionary.Data.Extensions;

[UsedImplicitly]
public static class DapperExtensions {
    public static Task<int> ExecAsync(
        this IDbConnection db, string storedProcedureName, object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.ExecuteAsync(new CommandDefinition(
            storedProcedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken
        ));
    }

    [ItemNotNull]
    public static Task<IEnumerable<T>> ExecQueryAsync<T>(
        this IDbConnection db,
        string storedProcedureName,
        object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.QueryAsync<T>(new CommandDefinition(
            storedProcedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken
        ));
    }

    [ItemNotNull]
    public static Task<T> ExecScalerAsync<T>(
        this IDbConnection db, string storedProcedureName, object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.ExecuteScalarAsync<T>(new CommandDefinition(
            storedProcedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken
        ));
    }

    [ItemNotNull]
    public static Task<T> ExecSingleAsync<T>(
        this IDbConnection db, string storedProcedureName, object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.QuerySingleAsync<T>(new CommandDefinition(
            storedProcedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken
        ));
    }

    [ItemCanBeNull]
    public static Task<T?> ExecSingleOrDefaultAsync<T>(
        this IDbConnection db,
        string storedProcedureName,
        object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.QuerySingleOrDefaultAsync<T?>(new CommandDefinition(
            storedProcedureName,
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken
        ));
    }

    public static Task<int> ExecuteAsync(
        this IDbConnection db, string commandText, object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.ExecuteAsync(new CommandDefinition(
            commandText,
            parameters,
            cancellationToken: cancellationToken
        ));
    }

    [ItemCanBeNull]
    public static Task<T?> ExecuteScalarAsync<T>(
        this IDbConnection db, string commandText, object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.ExecuteScalarAsync<T?>(new CommandDefinition(
            commandText,
            parameters,
            cancellationToken: cancellationToken
        ));
    }

    [ItemNotNull]
    public static Task<IEnumerable<T>> QueryAsync<T>(
        this IDbConnection db, string commandText, object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.QueryAsync<T>(new CommandDefinition(
            commandText,
            parameters,
            cancellationToken: cancellationToken
        ));
    }

    [ItemNotNull]
    public static Task<T> QuerySingleAsync<T>(
        this IDbConnection db, string commandText, object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.QuerySingleAsync<T>(new CommandDefinition(
            commandText,
            parameters,
            cancellationToken: cancellationToken
        ));
    }

    [ItemCanBeNull]
    public static Task<T?> QuerySingleOrDefaultAsync<T>(
        this IDbConnection db,
        string commandText,
        object? parameters = default,
        CancellationToken cancellationToken = default
    ) {
        return db.QuerySingleOrDefaultAsync<T?>(new CommandDefinition(
            commandText,
            parameters,
            cancellationToken: cancellationToken
        ));
    }
}
