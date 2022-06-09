using System.Text.Json;

using Fusionary.ClientSecrets;

using Xunit.Abstractions;

namespace Fusionary.Tests;

public class SecretsTest : TestBase {
    public SecretsTest(ITestOutputHelper outputHelper) : base(outputHelper)
    { }

    [Fact]
    public void Can_Verify_Wrong_Secret()
    {
        var creds = ClientSecretsManager.GenerateSecrets();

        var isMatch = ClientSecretsManager.VerifyClientSecret(creds.ClientSecret + "1", creds.AppSecret);

        Assert.False(isMatch);
    }

    [Fact]
    public void Can_Generate_Client_Credentials()
    {
        var creds = ClientSecretsManager.GenerateSecrets();

        var isMatch = ClientSecretsManager.VerifyClientSecret(creds.ClientSecret, creds.AppSecret);

        DumpObject(creds);

        Assert.True(isMatch);
    }
}
