namespace AppConfigFacility.Azure
{
    using Microsoft.WindowsAzure;

    public class AzureSettingsProvider : SettingsProviderBase
    {
        protected override string GetSetting(string key)
        {
            return CloudConfigurationManager.GetSetting(key);
        }
    }
}
