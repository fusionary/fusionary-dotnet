using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Moq;

namespace Fusionary.UnitTesting.Mocks;

public static class MockHttpContextAccessor
{
    public static IHttpContextAccessor Create()
    {
        Mock<IHttpContextAccessor> mock    = new();
        DefaultHttpContext         context = new();

        mock.Setup(_ => _.HttpContext).Returns(context);

        return mock.Object;
    }

    public static void SetUser(this IServiceProvider services, ClaimsPrincipal user)
    {
        var httpContext = services.GetRequiredService<IHttpContextAccessor>();

        if (httpContext.HttpContext != null)
        {
            httpContext.HttpContext.User = user;
        }
    }
}