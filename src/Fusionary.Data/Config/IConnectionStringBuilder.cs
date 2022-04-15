namespace Fusionary.Data.Config;

public interface IConnectionStringBuilder {
    public string GetConnectionString(DatabaseConfig config);
}