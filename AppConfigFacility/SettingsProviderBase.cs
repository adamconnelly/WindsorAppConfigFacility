namespace AppConfigFacility
{
    using System;

    /// <summary>
    /// A base class for <see cref="ISettingsProvider"/>s.
    /// </summary>
    public abstract class SettingsProviderBase : ISettingsProvider
    {
        /// <summary>
        /// Gets the specified setting.
        /// </summary>
        /// <param name="key">The setting key.</param>
        /// <param name="returnType">The type of the setting.</param>
        /// <returns>
        /// The setting.
        /// </returns>
        public object GetSetting(string key, Type returnType)
        {
            var value = GetSetting(key);

            if (returnType.IsEnum)
            {
                return Enum.Parse(returnType, value);
            }
            
            if (returnType == typeof(Uri))
            {
                return new Uri(value);
            }

            return Convert.ChangeType(value, returnType);
        }

        /// <summary>
        /// Gets the setting as a string from some config source.
        /// </summary>
        /// <param name="key">The key of the setting.</param>
        /// <returns>The string value of the setting.</returns>
        protected abstract string GetSetting(string key);
    }
}