namespace AppConfigFacility.Azure
{
    using Microsoft.WindowsAzure;

    /// <summary>
    /// A settings provider that gets its settings using <see cref="CloudConfigurationManager"/>.
    /// </summary>
    public class AzureSettingsProvider : SettingsProviderBase
    {
        /// <summary>
        /// Gets the setting as a string from some config source.
        /// </summary>
        /// <param name="key">The key of the setting.</param>
        /// <returns>The string value of the setting.</returns>
        protected override string GetSetting(string key)
        {
            return CloudConfigurationManager.GetSetting(key);
        }
    }
}
