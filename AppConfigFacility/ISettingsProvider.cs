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

        /// <summary>
        /// Gets the setting as a string from some config source.
        /// </summary>
        /// <param name="key">The key of the setting.</param>
        /// <returns>The string value of the setting.</returns>
        string GetSetting(string key);
    }
}
