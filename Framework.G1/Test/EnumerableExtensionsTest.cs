using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using FluentAssertions;
using Framework.G1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class EnumerableExtensionsTest
    {
        [TestMethod]
        public void EnumerateTestMethod()
        {
            5.Enumerate().Count().Should().Be(1);
            "hello".Enumerate().First().Should().Be("hello");
        }
    }
}
