using System.Text.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fusionary.UnitTesting;

public abstract class TestBase {

    protected TestBase(ITestOutputHelper outputHelper, string? environmentName = default)
    {
        if (string.IsNullOrWhiteSpace(environmentName)) {
            environmentName = Environments.Development;
        }

        Logger = outputHelper;
        JsonOptions = JsonHelper.CreateOptions();
        EnvironmentName = environmentName;
        
        var (configuration, services) = TestEnvBuilder.BuildEnv(
            outputHelper, 
            EnvironmentName, 
            BuildConfiguration,
            ConfigureServices);
        
        Configuration = configuration;
        Services = services;
    }

    protected virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    protected virtual void BuildConfiguration(IConfigurationBuilder builder)
    {
    }


    protected string EnvironmentName { get; init; } 

    protected IConfiguration Configuration { get; }

    protected ITestOutputHelper Logger { get; }

    protected JsonSerializerOptions JsonOptions { get; }

    protected IServiceProvider Services { get; }

    protected void LogMessage(string? message)
    {
        Logger.WriteLine(message ?? "<null>");
    }

    protected void DumpObject(object? value)
    {
        Logger.WriteLine(JsonSerializer.Serialize(value, JsonOptions));
    }
}