namespace WindsorAppConfigFacility
{
    using System;
    using System.Configuration;

    public class AppSettingsProvider : ISettingsProvider
    {
        public object GetSetting(string key, Type returnType)
        {
            if (returnType.IsEnum)
            {
                return Enum.Parse(returnType, ConfigurationManager.AppSettings[key]);
            }
            
            if (returnType == typeof(Uri))
            {
                return new Uri(ConfigurationManager.AppSettings[key]);
            }

            return Convert.ChangeType(ConfigurationManager.AppSettings[key], returnType);
        }
    }
}
