using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Conversion;

namespace AppConfigFacility.Azure
{
    using Microsoft.Azure;

    /// <summary>
    /// A settings provider that gets its settings using <see cref="CloudConfigurationManager"/>.
    /// </summary>
    public class AzureSettingsProvider : SettingsProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSettingsProvider"/> class.
        /// </summary>
        /// <param name="conversionManager">Used to convert settings to their required types.</param>
        public AzureSettingsProvider(IConversionManager conversionManager) : base(conversionManager)
        {
        }

        /// <inheritdoc />
        public override string GetSetting(string key)
        {
            return CloudConfigurationManager.GetSetting(key);
        }
    }
}
