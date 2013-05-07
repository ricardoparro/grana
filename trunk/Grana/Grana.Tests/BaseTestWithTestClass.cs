using System.Configuration;
using Grana.Model;

namespace Grana.Tests
{
    public class BaseTestWithTestClass<T> : BaseTest
    {
        public BaseTestWithTestClass()
        {
            
        }

        public BaseTestWithTestClass(bool autoMock) : base(autoMock, typeof(T))
        {
        }

        /// <summary>
        /// Is a new instance each time you call so can reconstruct with the new mocks
        /// </summary>
        protected T ClassToTest
        {
            get
            {
                T value = Resolve<T>();
                return value;
            }
        }

        /// <summary>
        /// Get a GranaDataContext that will not be disposed
        /// </summary>
        /// <returns></returns>
        protected GranaDataDataContext GetDataContextNotToBeDisposed()
        {
            string connection = ConfigurationManager.ConnectionStrings["GranaConnectionString"].ToString();
            GranaDataDataContext context = new GranaDataDataContext(connection);

            return context;
        }

        protected GranaDataDataContext GetDataContext()
        {
            return Resolve<GranaDataDataContext>();
        }
    }
}