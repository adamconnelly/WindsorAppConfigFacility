using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Conversion;

namespace AppConfigFacility
{
    using System;

    /// <summary>
    /// A base class for <see cref="ISettingsProvider"/>s.
    /// </summary>
    public abstract class SettingsProviderBase : ISettingsProvider
    {
        private readonly IConversionManager _conversionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsProviderBase"/> class.
        /// </summary>
        /// <param name="conversionManager">Used to convert settings to their required types.</param>
        protected SettingsProviderBase(IConversionManager conversionManager)
        {
            if (conversionManager == null)
            {
                throw new ArgumentNullException(nameof(conversionManager));
            }

            _conversionManager = conversionManager;
        }

        /// <summary>
        /// Gets the specified setting and converts it to the specified type.
        /// </summary>
        /// <param name="key">The setting key.</param>
        /// <param name="returnType">The type of the setting.</param>
        /// <returns>
        /// The setting.
        /// </returns>
        public virtual object GetSetting(string key, Type returnType)
        {
            var value = GetSetting(key);

            return ConvertSetting(value, returnType);
        }

        /// <inheritdoc />
        public abstract string GetSetting(string key);

        /// <summary>
        /// Converts the specified value into the specified type.
        /// </summary>
        /// <param name="value">The string representation.</param>
        /// <param name="returnType">The return type.</param>
        /// <returns>The value converted to the specified type.</returns>
        private object ConvertSetting(string value, Type returnType)
        {
            return _conversionManager.PerformConversion(value, returnType);
        }
    }
}