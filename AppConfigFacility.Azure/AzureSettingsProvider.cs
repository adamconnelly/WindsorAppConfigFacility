namespace AppConfigFacility.Azure
{
    using Microsoft.Azure;

    /// <summary>
    /// A settings provider that gets its settings using <see cref="CloudConfigurationManager"/>.
    /// </summary>
    public class AzureSettingsProvider : SettingsProviderBase
    {
        /// <inheritdoc />
        public override string GetSetting(string key)
        {
            return CloudConfigurationManager.GetSetting(key);
        }
    }
}
