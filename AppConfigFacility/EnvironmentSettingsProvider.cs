using System;

namespace AppConfigFacility
{
    /// <summary>
    /// Used to get settings from environment variables. If an environment variable doesn't exist
    /// or is empty for the specified key, the provider falls back to app settings.
    /// </summary>
    public class EnvironmentSettingsProvider : AppSettingsProvider
    {
        protected override string GetSetting(string key)
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
            {
                return Environment.GetEnvironmentVariable(key);
            }

            return base.GetSetting(key);
        }
    }
}
