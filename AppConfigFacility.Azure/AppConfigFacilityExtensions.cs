namespace AppConfigFacility.Azure
{
    using Microsoft.WindowsAzure;

    /// <summary>
    /// Extensions for the <see cref="AppConfigFacility"/>.
    /// </summary>
    public static class AppConfigFacilityExtensions
    {
        /// <summary>
        /// Indicates that we should get the settings using Azure's <see cref="CloudConfigurationManager"/>.
        /// </summary>
        /// <param name="facility">The facility.</param>
        public static void FromAzure(this AppConfigFacility facility)
        {
            facility.UseSettingsProvider<AzureSettingsProvider>();
        }
    }
}
