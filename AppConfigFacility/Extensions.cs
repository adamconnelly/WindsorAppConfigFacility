namespace AppConfigFacility
{
    using System;
    using Castle.MicroKernel.Registration;

    public static class Extensions
    {
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
            }

            return registration;
        }
    }
}
