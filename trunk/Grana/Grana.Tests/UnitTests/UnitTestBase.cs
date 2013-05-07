using Moq;

namespace Grana.Tests.UnitTests
{
    public abstract class UnitTestBase<T> : BaseTestWithTestClass<T>
    {
        protected UnitTestBase() : base(true)
        {
        }

        protected void AddNeverMockedType<TNeverMocked>()
        {
            MockContainer.AddNeverMockedType<TNeverMocked>();
        }

        protected override Mock<TInterface> CreateMockForAllProperties<TInterface>()
        {
            AddNeverMockedType<TInterface>();
            return base.CreateMockForAllProperties<TInterface>();
        }
    }
}
