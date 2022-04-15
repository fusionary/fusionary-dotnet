using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

using JetBrains.Annotations;

namespace Fusionary.Data.Extensions; 

[UsedImplicitly]
public static class MigrationExtensions {
    public static ICreateTableColumnOptionOrWithColumnSyntax AsNonNullableString(this ICreateTableColumnAsTypeSyntax migration, int? maxLength = null) {
        return maxLength.HasValue ? migration.AsString(maxLength.Value).NotNullable() : migration.AsString().NotNullable();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax AsPrimaryIdentity(this ICreateTableColumnAsTypeSyntax migration) {
        return migration.AsInt32().PrimaryKey().Identity();
    }

    public static void DropTableIfExists(this Migration migration, string tableName) {
        migration.Execute.Sql($"DROP TABLE IF EXISTS {tableName};");
    }
}