namespace AppConfigFacility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Castle.Core;
    using Castle.Core.Interceptor;
    using Castle.DynamicProxy;

    /// <summary>
    /// Intercepts calls to a settings interface to provide the values from the config.
    /// </summary>
    public class AppConfigInterceptor : IInterceptor, IOnBehalfAware
    {
        /// <summary>
        /// The key that should be used to store the computed properties.
        /// </summary>
        public static readonly string ComputedPropertiesKey = typeof (AppConfigInterceptor).FullName +
                                                              ".ComputedProperties";

        /// <summary>
        /// The key that should be used to store the settings prefix.
        /// </summary>
        public static readonly string PrefixKey = typeof (AppConfigInterceptor).FullName + "Prefix";

        private readonly ISettingsProvider _settingsProvider;
        private readonly ISettingsCache _settingsCache;

        private IDictionary _propertyAccessDictionary;
        private string _prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigInterceptor"/> type.
        /// </summary>
        /// <param name="settingsProvider">The settings provider.</param>
        /// <param name="settingsCache">The settings cache.</param>
        public AppConfigInterceptor(ISettingsProvider settingsProvider, ISettingsCache settingsCache)
        {
            _settingsProvider = settingsProvider;
            _settingsCache = settingsCache;
        }

        /// <summary>
        /// Intercepts the invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            if (!invocation.Method.Name.StartsWith("get_"))
            {
                throw new InvalidOperationException("The interceptor only supports property getters.");
            }

            var propertyName = invocation.Method.Name.Substring(4);

            object value;

            var cacheKey = GetCacheKey(propertyName, invocation.Method.DeclaringType);
            if (!_settingsCache.TryGetValue(cacheKey, out value))
            {
                value = GetValue(invocation, propertyName);

                _settingsCache.Put(cacheKey, value);
            }

            invocation.ReturnValue = value;
        }

        /// <summary>
        /// Used to get some extra information about the component being intercepted.
        /// </summary>
        /// <param name="target">The component being intercepted.</param>
        public void SetInterceptedComponentModel(ComponentModel target)
        {
            CreatePropertyAccessDictionary(target);
            _prefix = (string)target.ExtendedProperties[PrefixKey];
        }

        private static string GetCacheKey(string propertyName, Type targetType)
        {
            return targetType.FullName + "." + propertyName;
        }

        private object GetValue(IInvocation invocation, string propertyName)
        {
            Func<object, object> accessFunc = null;

            if (_propertyAccessDictionary != null)
            {
                accessFunc = (Func<object, object>)_propertyAccessDictionary[propertyName];
            }

            return accessFunc == null
                ? _settingsProvider.GetSetting(_prefix + propertyName, invocation.Method.ReturnType)
                : accessFunc(invocation.Proxy);
        }

        private void CreatePropertyAccessDictionary(ComponentModel target)
        {
            var accessDictionary = target.ExtendedProperties[ComputedPropertiesKey];
            if (accessDictionary != null)
            {
                _propertyAccessDictionary = (IDictionary) accessDictionary;
            }
            else
            {
                _propertyAccessDictionary = new Dictionary<string, Func<object, object>>();
            }
        }
    }
}
