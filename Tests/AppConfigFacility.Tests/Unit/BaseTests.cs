using NUnit.Framework;

namespace AppConfigFacility.Tests.Unit
{
    [TestFixture]
    public abstract class BaseTests
    {
        [SetUp]
        public void Setup()
        {
            OnSetup();
        }

        [TearDown]
        public void TearDown()
        {
            OnTearDown();
        }

        protected abstract void OnSetup();
        protected abstract void OnTearDown();
    }
}