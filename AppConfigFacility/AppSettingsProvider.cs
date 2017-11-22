namespace AppConfigFacility
{
    using System.Configuration;

    /// <summary>
    /// A settings provider that gets its settings from <see cref="ConfigurationManager.AppSettings"/>.
    /// </summary>
    public class AppSettingsProvider : SettingsProviderBase
    {
        /// <inheritdoc />
        public override string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
