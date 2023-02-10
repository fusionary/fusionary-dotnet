using System.Reflection;
using System.Text.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fusionary.UnitTesting;

public abstract class TestBase {
    private IHost TestHost { get; }

    protected TestBase(ITestOutputHelper outputHelper)
    {
        Logger = outputHelper;
        JsonOptions = JsonHelper.CreateOptions();

        TestHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, builder) => BuildConfiguration(builder))
            .ConfigureServices(
                (context, services) =>
                {
                    services.AddCommonServices(context.Configuration, outputHelper);
                    ConfigureServices(services, context.Configuration);
                }
            )
            .Build();
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