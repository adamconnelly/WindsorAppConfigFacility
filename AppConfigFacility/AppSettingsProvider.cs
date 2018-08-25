using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Conversion;

namespace AppConfigFacility
{
    using System.Configuration;

    /// <summary>
    /// A settings provider that gets its settings from <see cref="ConfigurationManager.AppSettings"/>.
    /// </summary>
    public class AppSettingsProvider : SettingsProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsProvider"/> class.
        /// </summary>
        /// <param name="conversionManager">Used to convert settings to their required types.</param>
        public AppSettingsProvider(IConversionManager conversionManager) : base(conversionManager)
        {
        }

        /// <inheritdoc />
        public override string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
