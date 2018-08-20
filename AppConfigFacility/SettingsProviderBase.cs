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
        protected SettingsProviderBase(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            ConversionManager = kernel.GetConversionManager();
        }

        protected IConversionManager ConversionManager { get; }

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
        protected virtual object ConvertSetting(string value, Type returnType)
        {
            return ConversionManager.PerformConversion(value, returnType);
        }
    }
}