using Microsoft.Extensions.Internal;

namespace Fusionary.UnitTesting.Mocks;

public class MockTime : ISystemClock
{
    private static DateTimeOffset? now;

    public DateTimeOffset UtcNow => now.GetValueOrDefault(DateTimeOffset.Now);

    public static void Reset()
    {
        now = null;
    }

    public static IDisposable SetCurrentTime(DateTimeOffset? currentTime)
    {
        now = currentTime;

        return new ResetToken();
    }

    private class ResetToken : IDisposable
    {
        public void Dispose()
        {
            Reset();
        }
    }
}