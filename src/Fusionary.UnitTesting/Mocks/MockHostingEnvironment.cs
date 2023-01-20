using Microsoft.Extensions.Hosting;

using Moq;

namespace Fusionary.UnitTesting.Mocks;

public static class MockHostingEnvironment
{
    public static IHostEnvironment Create(string? environmentName = null)
    {
        Mock<IHostEnvironment> mock = new();

        mock.Setup(x => x.EnvironmentName).Returns(environmentName ?? Environments.Staging);

        return mock.Object;
    }
}
