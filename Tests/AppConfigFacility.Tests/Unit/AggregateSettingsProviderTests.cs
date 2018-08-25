using System;
using System.Collections.Generic;
using System.Configuration;
using Castle.MicroKernel.SubSystems.Conversion;
using NUnit.Framework;

namespace AppConfigFacility.Tests.Unit
{
    [TestFixture]
    public class AggregateSettingsProviderTests
    {
        [Test]
        public void GetSetting_CanGetSettingFromAppSettings()
        {
            // Arrange
            const string key = "StringSetting";
            var expectedValue = ConfigurationManager.AppSettings[key];
            var conversionManager = new DefaultConversionManager();

            var provider = new AggregateSettingsProvider(conversionManager,
                new[] {new AppSettingsProvider(conversionManager)});

            // Act
            var value = provider.GetSetting(key, typeof(string));

            // Assert
            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void GetSetting_CanGetSettingFromEnvironmentInsteadOfAppSettings()
        {
            // Arrange
            const string key = "StringSetting";
            const string expectedValue = "EnvironmentValue";
            var conversionManager = new DefaultConversionManager();

            Environment.SetEnvironmentVariable(key, expectedValue);

            var provider = new AggregateSettingsProvider(conversionManager,
                new List<ISettingsProvider>
                {
                    new EnvironmentSettingsProvider(conversionManager),
                    new AppSettingsProvider(conversionManager)
                });

            // Act
            var value = provider.GetSetting(key, typeof(string));

            // Assert
            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void GetSetting_FallsBackToNextProviderIfFirstProviderHasNoValue()
        {
            // Arrange
            const string key = "StringSetting";
            var expectedValue = ConfigurationManager.AppSettings[key];
            var conversionManager = new DefaultConversionManager();

            Environment.SetEnvironmentVariable(key, null);

            var provider = new AggregateSettingsProvider(conversionManager,
                new List<ISettingsProvider>
                {
                    new EnvironmentSettingsProvider(conversionManager),
                    new AppSettingsProvider(conversionManager)
                });

            // Act
            var value = provider.GetSetting(key, typeof(string));

            // Assert
            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void Constructor_ThrowsExceptionIfNoProvidersAreSpecified()
        {
            var conversionManager = new DefaultConversionManager();
            Assert.Throws<ArgumentException>(() => new AggregateSettingsProvider(conversionManager, new List<ISettingsProvider>()));
            Assert.Throws<ArgumentException>(() => new AggregateSettingsProvider(conversionManager, null));
        }
    }
}
