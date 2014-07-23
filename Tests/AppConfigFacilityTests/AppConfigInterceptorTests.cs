namespace AppConfigFacility.Tests
{
    using System;
    using Castle.DynamicProxy;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class AppConfigInterceptorTests
    {
        private Mock<ISettingsProvider> _settingsProvider;
        private Mock<ISettingsCache> _settingsCache;

        [Test]
        public void Intercept_UsesSettingsProviderToGetSetting()
        {
            // Arrange
            var interceptor = CreateInterceptor();

            _settingsProvider.Setup(p => p.GetSetting("Name", typeof (string))).Returns("My Name");
            var invocation =
                Mock.Of<IInvocation>(i => i.Method.ReturnType == typeof (string) && i.Method.Name == "get_Name");

            // Act
            interceptor.Intercept(invocation);

            // Assert
            Assert.AreEqual("My Name", invocation.ReturnValue);
        }

        [Test]
        public void Intercept_ThrowsExceptionIfMethodNameNotInCorrectFormat()
        {
            // Arrange
            var interceptor = CreateInterceptor();

            var invocation =
                Mock.Of<IInvocation>(i => i.Method.Name == "DoSomething");

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() => interceptor.Intercept(invocation));

            // Assert
            Assert.AreEqual("The interceptor only supports property getters.", exception.Message);
        }

        [Test]
        public void Intercept_ReturnsSettingFromCacheIfPresent()
        {
            // Arrange
            object expectedName = "Test Name";
            var interceptor = CreateInterceptor();

            var invocation = Mock.Of<IInvocation>(i => i.Method.Name == "get_Name");

            _settingsCache.Setup(c => c.TryGetValue("Name", out expectedName)).Returns(true);

            // Act
            interceptor.Intercept(invocation);

            // Assert
            Assert.AreEqual(expectedName, invocation.ReturnValue);
        }

        [Test]
        public void Intercept_StoresValueInCache()
        {
            // Arrange
            const string expectedValue = "My Name";
            var interceptor = CreateInterceptor();

            _settingsProvider.Setup(p => p.GetSetting("Name", typeof(string))).Returns(expectedValue);
            var invocation =
                Mock.Of<IInvocation>(i => i.Method.ReturnType == typeof(string) && i.Method.Name == "get_Name");

            // Act
            interceptor.Intercept(invocation);

            // Assert
            _settingsCache.Verify(c => c.Put("Name", expectedValue));
        }

        private AppConfigInterceptor CreateInterceptor()
        {
            _settingsProvider = new Mock<ISettingsProvider>();
            _settingsCache = new Mock<ISettingsCache>();

            return new AppConfigInterceptor(_settingsProvider.Object, _settingsCache.Object);
        }
    }
}
