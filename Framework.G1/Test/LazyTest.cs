using FluentAssertions;
using Framework.G1;
using Framework.G1.Lazy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class LazyTest
    {
        [TestMethod]
        public void STRefTestMethod()
        {
            Test<STRef<My>>();
        }

        [TestMethod]
        public void MTRefTestMethod()
        {
            Test<MTRef<My>>();
        }

        private class My
        {            
        }

        private static void Test<T>()
            where T: struct, ILazy<My>
        {
            var my = new T();
            my.IsValueCrated.Should().BeFalse();
            my.Value.Should().NotBeNull();
            my.IsValueCrated.Should().BeTrue();            
        }
    }
}
