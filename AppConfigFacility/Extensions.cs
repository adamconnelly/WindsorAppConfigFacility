namespace AppConfigFacility
{
    using System;
    using Castle.MicroKernel.Registration;

    /// <summary>
    /// Contains extensions for registering Windsor components.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Indicates that the specified component should be automatically implemented from
        /// the config.
        /// </summary>
        /// <typeparam name="T">The type of the interface to implement.</typeparam>
        /// <param name="registration">The registration.</param>
        /// <param name="configAction">Allows you to customize how the settings interface is implemented.</param>
        /// <returns>The registration.</returns>
        public static ComponentRegistration<T> FromAppConfig<T>(this ComponentRegistration<T> registration,
            Action<IConfigConfiguration<T>> configAction = null)
            where T : class
        {
            registration.Interceptors<AppConfigInterceptor>();

            if (configAction != null)
            {
                var configuration = new ConfigConfiguration<T>();
                configAction(configuration);

                registration.ExtendedProperties(new Property(AppConfigInterceptor.ComputedPropertiesKey,
                    configuration.ComputedDictionary));
                registration.ExtendedProperties(new Property(AppConfigInterceptor.PrefixKey, configuration.Prefix));
            }

            return registration;
        }
    }
}
