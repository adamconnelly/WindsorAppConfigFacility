namespace AppConfigFacility.Azure
{
    public static class AppConfigFacilityExtensions
    {
        public static void FromAzure(this AppConfigFacility facility)
        {
            facility.UseSettingsProvider<AzureSettingsProvider>();
        }
    }
}
