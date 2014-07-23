namespace AppConfigFacility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Castle.Core;
    using Castle.Core.Interceptor;
    using Castle.DynamicProxy;

    public class AppConfigInterceptor : IInterceptor, IOnBehalfAware
    {
        public static readonly string ComputedPropertiesKey = typeof (AppConfigInterceptor).FullName +
                                                              ".ComputedProperties";

        private readonly ISettingsProvider _settingsProvider;
        private readonly ISettingsCache _settingsCache;

        private IDictionary _propertyAccessDictionary;

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
                value = GetValue(invocation, propertyName);

                // TODO: I think we should include the type name in the name of the setting in case
                // we have multiple settings interfaces with overlapping names
                _settingsCache.Put(propertyName, value);
            }

            invocation.ReturnValue = value;
        }

        private object GetValue(IInvocation invocation, string propertyName)
        {
            Func<object, object> accessFunc = null;

            if (_propertyAccessDictionary != null)
            {
                accessFunc = (Func<object, object>)_propertyAccessDictionary[propertyName];
            }

            return accessFunc == null
                ? _settingsProvider.GetSetting(propertyName, invocation.Method.ReturnType)
                : accessFunc(invocation.Proxy);
        }

        public void SetInterceptedComponentModel(ComponentModel target)
        {
            var accessDictionary = target.ExtendedProperties[ComputedPropertiesKey];
            if (accessDictionary != null)
            {
                _propertyAccessDictionary = (IDictionary)accessDictionary;
            }
            else
            {
                _propertyAccessDictionary = new Dictionary<string, Func<object, object>>();
            }
        }
    }
}
