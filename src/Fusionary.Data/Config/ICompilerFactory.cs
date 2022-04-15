using SqlKata.Compilers;

namespace Fusionary.Data.Config;

public interface ICompilerFactory {
    public Compiler CreateCompiler();
}
