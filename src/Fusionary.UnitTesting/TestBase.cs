using System.Reflection;
using System.Text.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fusionary.UnitTesting;

public abstract class TestBase {
    private IHostBuilder TestHostBuilder { get; }

    private IHost? _testHost;

    protected IHost TestHost => _testHost ??= TestHostBuilder.Build();

    protected TestBase(ITestOutputHelper outputHelper)
    {
        Logger = outputHelper;
        JsonOptions = JsonHelper.CreateOptions();

        TestHostBuilder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, builder) =>
                {
                    builder.AddInMemoryCollection(
                        new[] { new KeyValuePair<string, string?>("NETCORE_ENVIRONMENT", "Development"), }
                    );
                    BuildConfiguration(builder);
                }
            )
            .ConfigureServices(
                (context, services) =>
                {
                    services.AddCommonServices(context.Configuration, outputHelper);
                    ConfigureServices(services, context.Configuration);
                }
            );
    }

    protected virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    protected virtual IConfigurationBuilder BuildConfiguration(IConfigurationBuilder builder)
    {
        return builder;
    }

    protected IConfiguration Configuration => Services.GetRequiredService<IConfiguration>();

    protected ITestOutputHelper Logger { get; }

    protected JsonSerializerOptions JsonOptions { get; }

    protected IServiceProvider Services => TestHost.Services;

    protected void LogMessage(string? message)
    {
        Logger.WriteLine(message ?? "<null>");
    }

    protected void DumpObject(object? value)
    {
        Logger.WriteLine(JsonSerializer.Serialize(value, JsonOptions));
    }
}