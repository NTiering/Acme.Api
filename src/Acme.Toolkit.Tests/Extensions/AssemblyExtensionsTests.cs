using Acme.Toolkit.Extensions;
using System.Reflection;
using Xunit;

namespace Acme.Toolkit.Tests
{
    public class AssemblyExtensionsTests
    {
        private readonly Assembly _assembly;

        public AssemblyExtensionsTests()
        {
            _assembly = GetType().Assembly;
        }

        public interface IExampleInterface
        {
        }

        //---------------------------
        //     HELPERS
        //---------------------------
        public interface IExampleInterfaceOther : IExampleInterface
        {
        }

        [Fact]
        public void ItFindsTypes()
        {
            _assembly.GetTypesThatImplement<IExampleInterface>()
                .ShouldContain(typeof(ExampleClassOne));

            _assembly.GetTypesThatImplement<IExampleInterface>()
                .ShouldContain(typeof(ExampleClassTwo));
        }

        [Fact]
        public void ItIgnoresAbstractClasses()
        {
            _assembly.GetTypesThatImplement<IExampleInterface>()
                .ShouldNotContain(typeof(ExampleAbstractClass));
        }

        [Fact]
        public void ItIgnoresInterfaces()
        {
            _assembly.GetTypesThatImplement<IExampleInterface>()
                .ShouldNotContain(typeof(IExampleInterface));

            _assembly.GetTypesThatImplement<IExampleInterface>()
                .ShouldNotContain(typeof(IExampleInterfaceOther));
        }

        public abstract class ExampleAbstractClass : IExampleInterface
        {
        }

        public class ExampleClassOne : IExampleInterface
        {
        }

        public class ExampleClassTwo : ExampleAbstractClass
        {
        }
    }
}