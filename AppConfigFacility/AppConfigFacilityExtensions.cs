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
        public static void FromEnvironment(this AppConfigFacility facility)
        {
            facility.UseSettingsProvider<EnvironmentSettingsProvider>();
        }
    }
}
