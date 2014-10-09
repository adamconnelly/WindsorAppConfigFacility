namespace AppConfigFacility
{
    using System.Configuration;

    /// <summary>
    /// A settings provider that gets its settings from <see cref="ConfigurationManager.AppSettings"/>.
    /// </summary>
    public class AppSettingsProvider : SettingsProviderBase
    {
        /// <summary>
        /// Gets the setting as a string from some config source.
        /// </summary>
        /// <param name="key">The key of the setting.</param>
        /// <returns>The string value of the setting.</returns>
        protected override string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
