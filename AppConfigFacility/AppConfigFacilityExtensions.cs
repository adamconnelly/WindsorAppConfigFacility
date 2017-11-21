using System.Configuration;

namespace AppConfigFacility
{
    /// <summary>
    /// Extensions for the <see cref="AppConfigFacility"/>.
    /// </summary>
    public static class AppConfigFacilityExtensions
    {
        /// <summary>
        /// Indicates that we should get the settings from environment variables
        /// </summary>
        /// <param name="facility">The facility.</param>
        public static AppConfigFacility FromEnvironment(this AppConfigFacility facility)
        {
            facility.AddSettingsProvider<EnvironmentSettingsProvider>();

            return facility;
        }

        /// <summary>
        /// Indicates that we should get the settings from <see cref="ConfigurationManager.AppSettings"/>.
        /// </summary>
        /// <param name="facility">The facility.</param>
        public static AppConfigFacility FromAppSettings(this AppConfigFacility facility)
        {
            facility.AddSettingsProvider<AppSettingsProvider>();

            return facility;
        }
    }
}
