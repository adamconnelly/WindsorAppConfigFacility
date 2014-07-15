namespace WindsorAppConfigFacility
{
    using System;
    using Castle.MicroKernel.Facilities;
    using Castle.MicroKernel.Registration;

    public class AppConfigFacility : AbstractFacility
    {
        private Type _cacheType = typeof (DefaultSettingsCache);

        protected override void Init()
        {
            Kernel.Register(
                Component.For<ISettingsCache>().ImplementedBy(_cacheType),
                Component.For<ISettingsProvider>().ImplementedBy<AppSettingsProvider>(),
                Component.For<AppConfigInterceptor>().LifestyleTransient());
        }

        /// <summary>
        /// Enables caching of settings. This prevents multiple calls being made to the settings provider.
        /// </summary>
        public void CacheSettings()
        {
            _cacheType = typeof (MemorySettingsCache);
        }
    }
}
