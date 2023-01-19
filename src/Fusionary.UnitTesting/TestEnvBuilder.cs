using Fusionary.UnitTesting.Mocks;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;

namespace Fusionary.UnitTesting;

public static class TestEnvBuilder {
    public static (IConfiguration, IServiceProvider) BuildEnv(
        ITestOutputHelper outputHelper,
        string envName,
        Func<IConfigurationBuilder, IConfigurationBuilder>? configBuilder = default,
        Action<IServiceCollection, IConfiguration>? serviceBuilder = default
    )
    {
        var config   = BuildConfiguration(envName, configBuilder);
        var services = BuildServices(config, envName, outputHelper, serviceBuilder);

        return (config, services);
    }

    public static IServiceProvider BuildServices(
        IConfiguration configuration,
        string envName,
        ITestOutputHelper outputHelper,
        Action<IServiceCollection, IConfiguration>? customBuilder = null
    )
    {
        var services = new ServiceCollection();

        services.AddLogging(
            x => x.AddConfiguration(configuration).AddFilter("Fusionary", LogLevel.Trace).AddXunit(outputHelper)
        );

        services.AddSingleton<IDataProtectionProvider>(new EphemeralDataProtectionProvider());
        services.AddSingleton(MockHostingEnvironment.Create(envName));
        services.AddSingleton(MockHttpContextAccessor.Create());
        services.AddSingleton<ISystemClock>(new MockTime());
        services.AddMemoryCache();

        customBuilder?.Invoke(services, configuration);

        return services.BuildServiceProvider();
    }

    public static IConfiguration BuildConfiguration(string envName, Func<IConfigurationBuilder, IConfigurationBuilder>? customBuilder = null)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{envName}.json", true, true)
            .AddEnvironmentVariables();

        if (customBuilder is not null) {
           builder = customBuilder(builder);
        }

        return builder.Build();
    }
}
