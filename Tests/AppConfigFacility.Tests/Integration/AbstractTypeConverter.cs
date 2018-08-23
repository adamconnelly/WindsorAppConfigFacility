using System;
using Castle.Core.Configuration;
using Castle.MicroKernel.SubSystems.Conversion;

namespace AppConfigFacility.Tests.Integration
{
    /// <summary>
    ///     Base implementation of <see cref="T:Castle.MicroKernel.SubSystems.Conversion.ITypeConverter" />
    /// </summary>
    [Serializable]
    public abstract class AbstractTypeConverter : ITypeConverter
    {
        public ITypeConverterContext Context { get; set; }

        public abstract bool CanHandleType(Type type);

        public abstract object PerformConversion(string value, Type targetType);

        public abstract object PerformConversion(IConfiguration configuration, Type targetType);

        /// <summary>
        ///     Returns true if this instance of <c>ITypeConverter</c>
        ///     is able to handle the specified type with the specified
        ///     configuration
        /// </summary>
        /// <param name="type"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <remarks>
        ///     The default behavior is to just pass it to the normal CanHadnleType
        ///     peeking into the configuration is used for some advanced functionality
        /// </remarks>
        public virtual bool CanHandleType(Type type, IConfiguration configuration)
        {
            return CanHandleType(type);
        }

        public TTarget PerformConversion<TTarget>(string value)
        {
            return (TTarget) PerformConversion(value, typeof(TTarget));
        }

        public TTarget PerformConversion<TTarget>(IConfiguration configuration)
        {
            return (TTarget) PerformConversion(configuration, typeof(TTarget));
        }
    }
}