namespace AppConfigFacility.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using Castle.Core;
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
                Mock.Of<IInvocation>(
                    i =>
                        i.Method.DeclaringType == typeof (AppConfigInterceptorTests) && i.Method.ReturnType == typeof (string) &&
                        i.Method.Name == "get_Name");

            // Act
            interceptor.Intercept(invocation);

            // Assert
            Assert.AreEqual("My Name", invocation.ReturnValue);
        }

        [Test]
        public void Intercept_AppendsPrefixIfSpecified()
        {
            // Arrange
            var interceptor = CreateInterceptor();

            interceptor.SetInterceptedComponentModel(
                new ComponentModel(new ComponentName("AppConfigInterceptorTests", false),
                    new List<Type> {typeof (AppConfigInterceptorTests)}, typeof (AppConfigInterceptorTests),
                    new Dictionary<string, object> {{AppConfigInterceptor.PrefixKey, "SomePrefix:"}}));

            _settingsProvider.Setup(p => p.GetSetting("SomePrefix:Name", typeof(string))).Returns("My Name");
            var invocation =
                Mock.Of<IInvocation>(
                    i =>
                        i.Method.DeclaringType == typeof(AppConfigInterceptorTests) && i.Method.ReturnType == typeof(string) &&
                        i.Method.Name == "get_Name");

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

            var invocation = Mock.Of<IInvocation>(i => i.Method.Name == "get_Name" && i.Method.DeclaringType == typeof(AppConfigInterceptorTests));

            _settingsCache.Setup(c => c.TryGetValue(typeof(AppConfigInterceptorTests).FullName + ".Name", out expectedName)).Returns(true);

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
                Mock.Of<IInvocation>(
                    i =>
                        i.Method.ReturnType == typeof (string) && i.Method.Name == "get_Name" &&
                        i.Method.DeclaringType == typeof(AppConfigInterceptorTests));

            // Act
            interceptor.Intercept(invocation);

            // Assert
            _settingsCache.Verify(c => c.Put(typeof (AppConfigInterceptorTests).FullName + ".Name", expectedValue));
        }

        private AppConfigInterceptor CreateInterceptor()
        {
            _settingsProvider = new Mock<ISettingsProvider>();
            _settingsCache = new Mock<ISettingsCache>();

            return new AppConfigInterceptor(_settingsProvider.Object, _settingsCache.Object);
        }
    }
}
