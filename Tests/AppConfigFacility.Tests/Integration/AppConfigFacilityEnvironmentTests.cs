namespace AppConfigFacility.Tests.Integration
{
    using System;
    using System.Configuration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using NUnit.Framework;

    [TestFixture]
    public class AppConfigFacilityEnvironmentTests
    {
        private WindsorContainer _container;

        [Test]
        public void CanRetrieveStringPropertyFromConfig()
        {
            // Arrange
            Environment.SetEnvironmentVariable("StringSetting", "EnvironmentStringValue");
            var config = CreateConfig();

            // Act
            var result = config.StringSetting;

            // Assert
            Assert.AreEqual("EnvironmentStringValue", result);
        }

        private ITestConfig CreateConfig()
        {
            _container = new WindsorContainer();

            _container.AddFacility<AppConfigFacility>(f => f.FromEnvironment());

            _container.Register(Component.For<ITestConfig>().FromAppConfig());

            return _container.Resolve<ITestConfig>();
        }
    }
}
