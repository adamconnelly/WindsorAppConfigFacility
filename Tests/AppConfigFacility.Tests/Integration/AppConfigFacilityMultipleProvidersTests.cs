namespace AppConfigFacility.Tests.Integration
{
    using System;
    using System.Configuration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using NUnit.Framework;

    [TestFixture]
    public class AppConfigFacilityMultipleProvidersTests
    {
        private WindsorContainer _container;

        [Test]
        public void CanRetrievePropertyFromEnvironment()
        {
            // Arrange
            var config = CreateConfig();

            const string key = "StringSetting";
            const string value = "MyEnvironmentValue";

            Environment.SetEnvironmentVariable(key, value);

            // Act
            var result = config.StringSetting;

            // Assert
            Assert.AreEqual(value, result);
        }

        [Test]
        public void CanFallbackToAppSettings()
        {
            // Arrange
            var config = CreateConfig();

            const string key = "StringSetting";
            var value = ConfigurationManager.AppSettings[key];

            Environment.SetEnvironmentVariable(key, null);

            // Act
            var result = config.StringSetting;

            // Assert
            Assert.AreEqual(value, result);
        }

        [Test]
        public void WillAttemptToGetSettingFromEnvironmentBeforeAppSettings()
        {
            // Arrange
            var config = CreateConfig();

            const string key = "StringSetting";
            const string value = "EnvironmentSettingValue";

            Environment.SetEnvironmentVariable(key, value);

            // Act
            var result = config.StringSetting;

            // Assert
            Assert.AreEqual(value, result);
        }

        private ITestConfig CreateConfig()
        {
            _container = new WindsorContainer();

            _container.AddFacility<AppConfigFacility>(f => f.FromEnvironment().FromAppSettings());

            _container.Register(Component.For<ITestConfig>().FromAppConfig());

            return _container.Resolve<ITestConfig>();
        }
    }
}
