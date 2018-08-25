using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Conversion;

namespace AppConfigFacility
{
    using System;
    using Castle.MicroKernel.Facilities;
    using Castle.MicroKernel.Registration;

    /// <summary>
    /// A facility that allows you to create automatically implemented settings interfaces to
    /// access your application settings.
    /// </summary>
    public class AppConfigFacility : AbstractFacility
    {
        private Type _cacheType = typeof (DefaultSettingsCache);
        private readonly List<Type> _settingsProviderTypes = new List<Type>();

        /// <summary>
        /// The custom initialization for the Facility.
        /// </summary>
        /// <remarks>
        /// It must be overridden.
        /// </remarks>
        protected override void Init()
        {
            Kernel.Register(
                Component.For<ISettingsCache>().ImplementedBy(_cacheType),
                Component.For<ISettingsProvider>().UsingFactoryMethod(CreateSettingsProvider),
                Component.For<AppConfigInterceptor>().LifestyleTransient());
        }

        private ISettingsProvider CreateSettingsProvider(IKernel kernel)
        {
            if (!kernel.HasComponent(typeof(IConversionManager)))
            {
                kernel.Register(Component.For<IConversionManager>().UsingFactoryMethod(k => k.GetConversionManager()));
            }

            var conversionManager = kernel.Resolve<IConversionManager>();

            IReadOnlyCollection<ISettingsProvider> providers;
            if (_settingsProviderTypes.Any())
            {
                providers = _settingsProviderTypes.Select(type =>
                {
                    kernel.Register(Component.For(type));
                    return (ISettingsProvider) kernel.Resolve(type);
                }).ToList().AsReadOnly();
            }
            else
            {
                providers = new ReadOnlyCollection<ISettingsProvider>(
                    new List<ISettingsProvider>(new[] {new AppSettingsProvider(conversionManager)}));
            }

            return new AggregateSettingsProvider(conversionManager, providers);
        }

        /// <summary>
        /// Enables caching of settings. This prevents multiple calls being made to the settings provider.
        /// </summary>
        public AppConfigFacility CacheSettings()
        {
            _cacheType = typeof (MemorySettingsCache);
            return this;
        }

        /// <summary>
        /// Adds a new settings provider to the facility. The facility will attempt to get the setting
        /// from each provider in turn based on the order they were added. So it will try to get the setting
        /// from the provider that was added first, and then will fallback to the next provider, and so on.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the settings provider.
        /// </typeparam>
        public AppConfigFacility AddSettingsProvider<T>() where T : ISettingsProvider
        {
            _settingsProviderTypes.Add(typeof(T));
            return this;
        }
    }
}
