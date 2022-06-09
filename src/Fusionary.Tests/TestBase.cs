using System.Text.Json;

using Xunit.Abstractions;

namespace Fusionary.Tests;

public abstract class TestBase {
    private static readonly JsonSerializerOptions JsonOptions = new() {
        WriteIndented = true,
    };
    
    protected ITestOutputHelper _outputHelper;

    protected TestBase(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    protected void LogMessage(string message) => _outputHelper.WriteLine(message);
    
    protected void DumpObject(object value) =>  LogMessage(JsonSerializer.Serialize(value, JsonOptions));
}
