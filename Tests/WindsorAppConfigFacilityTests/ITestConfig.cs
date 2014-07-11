namespace WindsorAppConfigFacilityTests
{
    using System;

    public interface ITestConfig
    {
        string StringSetting { get; }
        int IntSetting { get; }
        TestEnum EnumSetting { get; }
        bool BoolSetting { get; }
        Uri UriSetting { get; }
        string ComputedSetting { get; }
    }
}
