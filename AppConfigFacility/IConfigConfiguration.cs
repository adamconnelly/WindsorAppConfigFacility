namespace AppConfigFacility
{
    using System;
    using System.Linq.Expressions;

    public interface IConfigConfiguration<T>
    {
        /// <summary>
        /// Defines a function used to get the specified property.
        /// </summary>
        /// <typeparam name="TP">The return type of the property.</typeparam>
        /// <param name="propertyExpression">An expression defining the property to compute.</param>
        /// <param name="accessFunc">The function used to get the property value.</param>
        /// <returns>The config instance.</returns>
        IConfigConfiguration<T> Computed<TP>(Expression<Func<T, TP>> propertyExpression, Func<T, object> accessFunc);

        /// <summary>
        /// Defines a prefix to use when reading settings.
        /// </summary>
        /// <param name="prefix">The prefix to use.</param>
        /// <returns>The config instance.</returns>
        IConfigConfiguration<T> WithPrefix(string prefix);
    }
}
