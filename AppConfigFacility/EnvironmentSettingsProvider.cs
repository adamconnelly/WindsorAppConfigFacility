using System;
using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Conversion;

namespace AppConfigFacility
{
    /// <summary>
    /// Used to get settings from environment variables. If an environment variable doesn't exist
    /// or is empty for the specified key, the provider falls back to app settings.
    /// </summary>
    public class EnvironmentSettingsProvider : SettingsProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentSettingsProvider"/> class.
        /// </summary>
        /// <param name="conversionManager">Used to convert settings to their required types.</param>
        public EnvironmentSettingsProvider(IConversionManager conversionManager) : base(conversionManager)
        {
        }

        /// <inheritdoc />
        public override string GetSetting(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
