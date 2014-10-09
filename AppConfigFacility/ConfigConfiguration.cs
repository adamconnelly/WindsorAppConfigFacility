namespace AppConfigFacility
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Used to get custom config information about a settings interface.
    /// </summary>
    /// <typeparam name="T">The type of the interface.</typeparam>
    public class ConfigConfiguration<T> : IConfigConfiguration<T>
    {
        private readonly Dictionary<string, Func<object, object>> _computedDictionary = new Dictionary<string, Func<object, object>>();

        /// <summary>
        /// Gets a mapping of property names to the function used to implement them.
        /// </summary>
        public Dictionary<string, Func<object, object>> ComputedDictionary
        {
            get { return _computedDictionary; }
        }

        /// <summary>
        /// Gets the prefix that should be used to access the settings for the interface.
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// Defines a function used to get the specified property.
        /// </summary>
        /// <typeparam name="TP">The return type of the property.</typeparam>
        /// <param name="propertyExpression">An expression defining the property to compute.</param>
        /// <param name="accessFunc">The function used to get the property value.</param>
        /// <returns>The config instance.</returns>
        public IConfigConfiguration<T> Computed<TP>(Expression<Func<T, TP>> propertyExpression, Func<T, object> accessFunc)
        {
            // We need to wrap the accessFunc in another Func because the interceptor doesn't know what type it's
            // intercepting, so it needs a Func<object, object>.
            _computedDictionary[GetPropertyName(propertyExpression)] = o => accessFunc((T) o);
            return this;
        }

        /// <summary>
        /// Defines a prefix to use when reading settings.
        /// </summary>
        /// <param name="prefix">The prefix to use.</param>
        /// <returns>The config instance.</returns>
        public IConfigConfiguration<T> WithPrefix(string prefix)
        {
            Prefix = prefix;
            return this;
        }

        private static string GetPropertyName<TP>(Expression<Func<T, TP>> propertyExpression)
        {
            return ((MemberExpression) propertyExpression.Body).Member.Name;
        }
    }
}
