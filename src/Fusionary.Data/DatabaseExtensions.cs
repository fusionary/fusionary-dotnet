using Fusionary.Data.Config;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fusionary.Data; 

public static class UseDatabaseExtensions {
    public static DatabaseConfig UseDatabaseConfig(this  IServiceCollection services, IConfiguration configuration, string databaseConfigName = "Database")
    {
        var configSection = configuration.GetSection(databaseConfigName);
        
        services.Configure<DatabaseConfig>(configSection);
        
        return configSection.Get<DatabaseConfig>();
    }
}
