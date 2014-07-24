namespace AppConfigFacility.Tests.Integration
{
    using System;
    using System.Configuration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using NUnit.Framework;

    [TestFixture]
    public class AppConfigFacilityTests
    {
        private WindsorContainer _container;

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

        [Test]
        public void CanRetrieveEnumPropertyFromConfig()
        {
            // Arrange
            var config = CreateConfig();

            // Act
            var result = config.EnumSetting;

            // Assert
            var expected = Enum.Parse(typeof (TestEnum), ConfigurationManager.AppSettings["EnumSetting"]);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CanRetrieveBooleanPropertyFromConfig()
        {
            // Arrange
            var config = CreateConfig();

            // Act
            var result = config.BoolSetting;

            // Assert
            var expected = bool.Parse(ConfigurationManager.AppSettings["BoolSetting"]);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CanRetrieveUrlPropertyFromConfig()
        {
            // Arrange
            var config = CreateConfig();

            // Act
            var result = config.UriSetting;

            // Assert
            var expected = new Uri(ConfigurationManager.AppSettings["UriSetting"]);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RegistersDefaultSettingsCacheByDefault()
        {
            // Arrange
            CreateConfig();

            // Act
            var cache = _container.Resolve<ISettingsCache>();

            // Assert
            Assert.IsInstanceOf<DefaultSettingsCache>(cache);
        }

        [Test]
        public void CanConfigureCaching()
        {
            // Arrange
            var container = new WindsorContainer();

            container.AddFacility<AppConfigFacility>(f => f.CacheSettings());

            // Act
            var cache = container.Resolve<ISettingsCache>();

            // Assert
            Assert.IsInstanceOf<MemorySettingsCache>(cache);
        }

        private ITestConfig CreateConfig()
        {
            _container = new WindsorContainer();

            _container.AddFacility<AppConfigFacility>();

            _container.Register(Component.For<ITestConfig>().FromAppConfig(
                // TODO: Something like the following for creating computed settings
                //c => c.Configure(t => t.ComputedSetting).ComputedBy(t => t.StringSetting + " " + t.IntSetting)
                ));

            return _container.Resolve<ITestConfig>();
        }
    }
}
