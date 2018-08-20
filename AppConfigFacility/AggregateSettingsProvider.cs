using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;

namespace AppConfigFacility
{
    /// <summary>
    /// A wrapper around several <see cref="ISettingsProvider"/> objects that will attempt to get
    /// settings from each one in turn.
    /// </summary>
    public class AggregateSettingsProvider : SettingsProviderBase
    {
        private readonly IReadOnlyCollection<ISettingsProvider> _settingsProviders;

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateSettingsProvider"/> class.
        /// </summary>
        /// <param name="kernel">Our current IoC Castle Windsor container</param>
        /// <param name="settingsProviders">
        /// The list of settings providers to use to get the settings. They will be iterated over in turn,
        /// and the first non-null result will be returned when retrieving a setting.
        /// </param>
        public AggregateSettingsProvider(IKernel kernel, IReadOnlyCollection<ISettingsProvider> settingsProviders) : base(kernel)
        {
            if (settingsProviders == null || !settingsProviders.Any())
            {
                throw new ArgumentException("At least one settings provider must be specified", nameof(settingsProviders));
            }

            _settingsProviders = settingsProviders;
        }

        /// <summary>
        /// Gets the collection of providers the aggregate provider is using.
        /// </summary>
        public IReadOnlyCollection<ISettingsProvider> SettingsProviders => _settingsProviders;

        public override string GetSetting(string key)
        {
            foreach (var provider in _settingsProviders)
            {
                var value = provider.GetSetting(key);
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }
    }
}
