namespace AppConfigFacility.Tests
{
    using System;

    public interface ITestConfig
    {
        string StringSetting { get; }
        int IntSetting { get; }
        TestEnum EnumSetting { get; }
        bool BoolSetting { get; }
        TimeSpan TimeSpanSetting { get; }
        Version VersionSetting { get; }
        Uri UriSetting { get; }
        string ComputedSetting { get; }
    }
}
