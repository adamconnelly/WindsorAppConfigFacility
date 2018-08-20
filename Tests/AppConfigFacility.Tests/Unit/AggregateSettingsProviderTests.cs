using System;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;

namespace AppConfigFacility.Tests.Unit
{
    public class AggregateSettingsProviderTests : BaseProviderTests
    {
        [Test]
        public void GetSetting_CanGetSettingFromAppSettings()
        {
            // Arrange
            const string key = "StringSetting";
            var expectedValue = ConfigurationManager.AppSettings[key];

            var provider = CreateAggregateSettingsProvider(new[] {typeof(AppSettingsProvider)});

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

            Environment.SetEnvironmentVariable(key, expectedValue);

            var provider = CreateAggregateSettingsProvider(
                new[]
                {
                    typeof(EnvironmentSettingsProvider),
                    typeof(AppSettingsProvider)
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

            Environment.SetEnvironmentVariable(key, null);

            var provider = CreateAggregateSettingsProvider(
                new[]
                {
                    typeof(EnvironmentSettingsProvider),
                    typeof(AppSettingsProvider)
                });

            // Act
            var value = provider.GetSetting(key, typeof(string));

            // Assert
            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void Constructor_ThrowsExceptionIfNoProvidersAreSpecified()
        {
            Assert.Throws<ArgumentException>(() => new AggregateSettingsProvider(Container.Kernel, new List<ISettingsProvider>()));
            Assert.Throws<ArgumentException>(() => new AggregateSettingsProvider(Container.Kernel, null));
        }
    }
}
