namespace AppConfigFacility.Tests.Integration
{
    using System.Configuration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using NUnit.Framework;

    [TestFixture]
    public class AppConfigFacilityPrefixTests
    {
        [Test]
        public void ShouldUsePrefixToGetSettingIfSpecified()
        {
            var container = new WindsorContainer()
                .AddFacility<AppConfigFacility>();

            container.Register(Component.For<ISearchConfig>().FromAppConfig(c => c.WithPrefix("Search:")));

            var config = container.Resolve<ISearchConfig>();

            Assert.AreEqual(ConfigurationManager.AppSettings["Search:Url"], config.Url);
        }
    }
}
