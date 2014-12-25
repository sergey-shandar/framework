using System;
using FluentAssertions;
using Framework.G1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class ObjectExtensionsTest
    {
        [TestMethod]
        public void IsNullTestMethod()
        {
            ((object)null).IsNull().Should().BeTrue();
            "hello".IsNull().Should().BeFalse();
        }
    }
}
