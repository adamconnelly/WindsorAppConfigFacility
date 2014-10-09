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
        private Type _settingsProviderType = typeof (AppSettingsProvider);

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
                Component.For<ISettingsProvider>().ImplementedBy(_settingsProviderType),
                Component.For<AppConfigInterceptor>().LifestyleTransient());
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
        /// Specified the type of settings provider to use.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the settings provider.
        /// </typeparam>
        public AppConfigFacility UseSettingsProvider<T>() where T : ISettingsProvider
        {
            _settingsProviderType = typeof (T);
            return this;
        }
    }
}
