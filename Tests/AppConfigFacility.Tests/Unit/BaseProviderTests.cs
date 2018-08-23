using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace AppConfigFacility.Tests.Unit
{
    public abstract class BaseProviderTests : BaseTests
    {
        private WindsorContainer _container;

        protected override void OnSetup()
        {
            _container = new WindsorContainer();
            _container
                .Register(
                    Component.For<EnvironmentSettingsProvider>(),
                    Component.For<AppSettingsProvider>(),
                    Component.For<AggregateSettingsProvider>());
        }

        protected override void OnTearDown()
        {
            _container.Dispose();
            _container = null;
        }

        protected IWindsorContainer Container
        {
            get { return _container; }
        }

        protected AggregateSettingsProvider CreateAggregateSettingsProvider(IEnumerable<Type> providerTypes)
        {
            IReadOnlyCollection<ISettingsProvider> settingsProviders =
                (providerTypes ?? Enumerable.Empty<Type>())
                .Select(providerType => (ISettingsProvider) _container.Resolve(providerType))
                .ToList();

            return _container.Resolve<AggregateSettingsProvider>(new Arguments().Insert("settingsProviders", settingsProviders));
        }

        protected AppSettingsProvider AppSettingsProvider
        {
            get { return _container.Resolve<AppSettingsProvider>(); }
        }

        protected EnvironmentSettingsProvider EnvironmentSettingsProvider
        {
            get { return _container.Resolve<EnvironmentSettingsProvider>(); }
        }
    }
}