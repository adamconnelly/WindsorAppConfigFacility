namespace AppConfigFacility
{
    using System.Configuration;

    public class AppSettingsProvider : SettingsProviderBase
    {
        protected override string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
