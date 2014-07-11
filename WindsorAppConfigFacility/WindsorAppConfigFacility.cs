namespace WindsorAppConfigFacility
{
    using Castle.MicroKernel.Facilities;
    using Castle.MicroKernel.Registration;

    public class AppConfigFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Register(
                Component.For<ISettingsProvider>().ImplementedBy<AppSettingsProvider>(),
                Component.For<AppConfigInterceptor>().LifestyleTransient());
        }
    }
}
