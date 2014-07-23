namespace AppConfigFacility
{
    using System;
    using Castle.MicroKernel.Facilities;
    using Castle.MicroKernel.Registration;

    public class AppConfigFacility : AbstractFacility
    {
        private Type _cacheType = typeof (DefaultSettingsCache);
        private Type _settingsProviderType = typeof (AppSettingsProvider);

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
        public void CacheSettings()
        {
            _cacheType = typeof (MemorySettingsCache);
        }

        /// <summary>
        /// Specified the type of settings provider to use.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the settings provider.
        /// </typeparam>
        public void UseSettingsProvider<T>() where T : ISettingsProvider
        {
            _settingsProviderType = typeof (T);
        }
    }
}
