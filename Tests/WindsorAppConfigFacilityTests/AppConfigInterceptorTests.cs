namespace WindsorAppConfigFacilityTests
{
    using System;
    using Castle.DynamicProxy;
    using Moq;
    using NUnit.Framework;
    using WindsorAppConfigFacility;

    [TestFixture]
    public class AppConfigInterceptorTests
    {
        private Mock<ISettingsProvider> _settingsProvider;

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

        private AppConfigInterceptor CreateInterceptor()
        {
            _settingsProvider = new Mock<ISettingsProvider>();
            return new AppConfigInterceptor(_settingsProvider.Object);
        }
    }
}
