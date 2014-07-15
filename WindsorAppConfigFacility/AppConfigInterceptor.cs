namespace WindsorAppConfigFacility
{
    using System;
    using Castle.DynamicProxy;

    public class AppConfigInterceptor : IInterceptor
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly ISettingsCache _settingsCache;

        public AppConfigInterceptor(ISettingsProvider settingsProvider, ISettingsCache settingsCache)
        {
            _settingsProvider = settingsProvider;
            _settingsCache = settingsCache;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!invocation.Method.Name.StartsWith("get_"))
            {
                throw new InvalidOperationException("The interceptor only supports property getters.");
            }

            var propertyName = invocation.Method.Name.Substring(4);

            object value;

            if (!_settingsCache.TryGetValue(propertyName, out value))
            {
                value = _settingsProvider.GetSetting(propertyName, invocation.Method.ReturnType);
                _settingsCache.Put(propertyName, value);
            }

            invocation.ReturnValue = value;
        }
    }
}
