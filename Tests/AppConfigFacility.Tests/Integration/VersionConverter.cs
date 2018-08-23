using System;
using Castle.Core.Configuration;

namespace AppConfigFacility.Tests.Integration
{
    [Serializable]
    public class VersionConverter : AbstractTypeConverter
    {
        public override bool CanHandleType(Type type)
        {
            return type == typeof(Version);
        }

        public override object PerformConversion(string value, Type targetType)
        {
            return Version.Parse(value);
        }

        public override object PerformConversion(IConfiguration configuration, Type targetType)
        {
            return PerformConversion(configuration.Value, targetType);
        }
    }
}