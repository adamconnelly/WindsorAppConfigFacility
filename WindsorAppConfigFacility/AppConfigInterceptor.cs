namespace WindsorAppConfigFacility
{
    using System;
    using System.Configuration;
    using Castle.DynamicProxy;

    public class AppConfigInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var propertyName = invocation.Method.Name.Substring(4);

            invocation.ReturnValue = Convert.ChangeType(ConfigurationManager.AppSettings[propertyName],
                invocation.Method.ReturnType);
        }
    }
}
