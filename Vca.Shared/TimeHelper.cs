using System.Diagnostics.CodeAnalysis;

namespace Vca.Shared
{
    [ExcludeFromCodeCoverage]
    public static class TimeHelper
    {
        public static DateTime ServerTimeNow => DateTime.Now.ToUniversalTime();
    }
}
