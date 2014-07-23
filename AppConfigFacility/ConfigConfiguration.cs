namespace AppConfigFacility
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class ConfigConfiguration<T> : IConfigConfiguration<T>
    {
        private readonly Dictionary<string, Func<object, object>> _computedDictionary = new Dictionary<string, Func<object, object>>();

        public IConfigConfiguration<T> Computed<TP>(Expression<Func<T, TP>> propertyExpression, Func<T, object> accessFunc)
        {
            // We need to wrap the accessFunc in another Func because the interceptor doesn't know what type it's
            // intercepting, so it needs a Func<object, object>.
            _computedDictionary[GetPropertyName(propertyExpression)] = o => accessFunc((T) o);
            return this;
        }

        public Dictionary<string, Func<object, object>> ComputedDictionary
        {
            get { return _computedDictionary; }
        }

        private static string GetPropertyName<TP>(Expression<Func<T, TP>> propertyExpression)
        {
            return ((MemberExpression) propertyExpression.Body).Member.Name;
        }
    }
}
