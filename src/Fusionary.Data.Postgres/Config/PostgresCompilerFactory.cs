using Fusionary.Data.Config;

using SqlKata.Compilers;

namespace Fusionary.Data.Postgres.Config;

public class PostgresCompilerFactory : ICompilerFactory {
    /// <inheritdoc />
    public Compiler CreateCompiler()
    {
        return new PostgresCompiler();
    }
}