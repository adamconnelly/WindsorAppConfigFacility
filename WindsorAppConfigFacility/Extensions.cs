namespace WindsorAppConfigFacility
{
    using Castle.MicroKernel.Registration;

    public static class Extensions
    {
        public static ComponentRegistration<T> FromAppConfig<T>(this ComponentRegistration<T> registration)
            where T: class
        {
            registration.Interceptors<AppConfigInterceptor>();

            return registration;
        }
    }
}
