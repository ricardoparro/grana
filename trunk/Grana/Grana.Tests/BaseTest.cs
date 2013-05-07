using System;
using Autofac;
using Grana.Service;
using Moq;

namespace Grana.Tests
{
    public class BaseTest : IDisposable
    {
        protected static MoqContainer MockContainer { get; private set; }
        protected ILifetimeScope LifetimeScope;

        public BaseTest()
        {
            MockContainer = new MoqContainer();
            LifetimeScope = MockContainer.DependencyResolver.BeginLifetimeScope();
        }

        public BaseTest(bool autoMock, Type typeTesting)
        {
            MockContainer = new MoqContainer(autoMock, typeTesting);
            LifetimeScope = MockContainer.DependencyResolver.BeginLifetimeScope();
        }

        protected ILifetimeScope BeginScope()
        {
            LifetimeScope = MockContainer.StartNewLifetime();
            return LifetimeScope;
        }

        protected T Resolve<T>()
        {
            return ServiceModule.ResolveReference<T>(LifetimeScope);
        }

        protected void InjectMock(object o)
        {
            MockContainer.InjectMock(o);
        }

        protected virtual Mock<TInterface> CreateMockForAllProperties<TInterface>() where TInterface : class 
        {
            TInterface concreteClass = Resolve<TInterface>();
            Mock<TInterface> mockAllPropsCallBase = MoqBuilder.CreateMockAllPropsCallBase(concreteClass);

            return mockAllPropsCallBase;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            MockContainer.Dispose();
        }

        #endregion
    }
}
