using System.Diagnostics.CodeAnalysis;

namespace Vca.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseSpecification
    {
        [TestInitialize]
        public void Setup()
        {
            InitAppSettings();
            Given();
            When();
        }

        protected virtual void Given()
        {
        }

        protected virtual void When()
        {
        }

        private static void InitAppSettings()
        {
        }
    }
}
