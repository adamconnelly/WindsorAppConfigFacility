using System;
using NUnit.Framework;

namespace AppConfigFacility.Tests.Unit
{
    public class EnvironmentSettingsProviderTests : BaseProviderTests
    {
        [Test]
        public void CanGetSettingFromEnvironment()
        {
            // Arrange
            const string expectedValue = "Test123";
            const string variableName = "AppConfigFacilityTestVar";

            Environment.SetEnvironmentVariable(variableName, expectedValue);
            var provider = EnvironmentSettingsProvider;

            // Act
            var setting = (string)provider.GetSetting(variableName, typeof(string));

            // Assert
            Assert.AreEqual(expectedValue, setting);
        }

        [Test]
        public void ReturnsNullIfEnvironmentVariableNotSet()
        {
            // Arrange
            const string variableName = "StringSetting";
            
            var provider = EnvironmentSettingsProvider;

            // Act
            var setting = (string)provider.GetSetting(variableName, typeof(string));

            // Assert
            Assert.IsNull(setting);
        }
    }
}
