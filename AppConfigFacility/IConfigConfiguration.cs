namespace AppConfigFacility
{
    using System;
    using System.Linq.Expressions;

    public interface IConfigConfiguration<T>
    {
        IConfigConfiguration<T> Computed<TP>(Expression<Func<T, TP>> propertyExpression, Func<T, object> accessFunc);
    }
}
