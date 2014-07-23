namespace AppConfigFacility
{
    using System;

    /// <summary>
    /// Used to get app settings from somewhere.
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Gets the specified setting.
        /// </summary>
        /// <param name="key">The setting key.</param>
        /// <param name="returnType">The type of the setting.</param>
        /// <returns>
        /// The setting.
        /// </returns>
        object GetSetting(string key, Type returnType);
    }
}
