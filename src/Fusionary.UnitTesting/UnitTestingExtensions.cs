using Fusionary.UnitTesting.Mocks;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;

namespace Fusionary.UnitTesting;

public static class UnitTestingExtensions
{
    public static void AddCommonServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ITestOutputHelper outputHelper
    )
    {
        services.AddLogging(
            x => x.AddConfiguration(configuration).AddFilter("Fusionary", LogLevel.Trace).AddXunit(outputHelper)
        );

        services.AddSingleton<IDataProtectionProvider>(new EphemeralDataProtectionProvider());
        services.AddSingleton(MockHttpContextAccessor.Create());
        services.AddSingleton<ISystemClock>(new MockTime());
        services.AddMemoryCache();
    }
}