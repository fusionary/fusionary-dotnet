using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Moq;

namespace Fusionary.UnitTesting.Mocks;

public static class MockHostingEnvironment
{
    public static IWebHostEnvironment Create(string? environmentName = null)
    {
        Mock<IWebHostEnvironment> mock = new();

        mock.Setup(x => x.EnvironmentName).Returns(environmentName ?? Environments.Staging);

        return mock.Object;
    }
}
