using System;
using Castle.MicroKernel;

namespace AppConfigFacility
{
    /// <summary>
    /// Used to get settings from environment variables. If an environment variable doesn't exist
    /// or is empty for the specified key, the provider falls back to app settings.
    /// </summary>
    public class EnvironmentSettingsProvider : SettingsProviderBase
    {
        public EnvironmentSettingsProvider(IKernel kernel) : base(kernel)
        {
        }

        /// <inheritdoc />
        public override string GetSetting(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}