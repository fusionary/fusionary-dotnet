using System.Data.Common;

using SqlKata.Compilers;

namespace Fusionary.Data.Config;

public interface IDataProviderConfig: ICompilerFactory, IConnectionStringBuilder, IConnectionFactory {

}
