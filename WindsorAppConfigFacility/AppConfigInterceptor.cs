namespace WindsorAppConfigFacility
{
    using System;
    using Castle.DynamicProxy;

    public class AppConfigInterceptor : IInterceptor
    {
        private readonly ISettingsProvider _settingsProvider;

        public AppConfigInterceptor(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!invocation.Method.Name.StartsWith("get_"))
            {
                throw new InvalidOperationException("The interceptor only supports property getters.");
            }

            var propertyName = invocation.Method.Name.Substring(4);

            invocation.ReturnValue = _settingsProvider.GetSetting(propertyName, invocation.Method.ReturnType);
        }
    }
}
