namespace WindsorAppConfigFacilityTests
{
    using System.Configuration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using NUnit.Framework;
    using WindsorAppConfigFacility;

    [TestFixture]
    public class AppConfigFacilityTests
    {
        [Test]
        public void CanRetrieveStringPropertyFromConfig()
        {
            // Arrange
            var config = CreateConfig();

            // Act
            var result = config.StringSetting;

            // Assert
            Assert.AreEqual(ConfigurationManager.AppSettings["StringSetting"], result);
        }

        [Test]
        public void CanRetrieveIntPropertyFromConfig()
        {
            // Arrange
            var config = CreateConfig();

            // Act
            var result = config.IntSetting;

            // Assert
            Assert.AreEqual(int.Parse(ConfigurationManager.AppSettings["IntSetting"]), result);
        }

        private ITestConfig CreateConfig()
        {
            var container = new WindsorContainer();

            container.AddFacility<AppConfigFacility>();

            container.Register(Component.For<ITestConfig>().FromAppConfig());

            return container.Resolve<ITestConfig>();
        }
    }
}
