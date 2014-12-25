using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.G1;
using FluentAssertions;

namespace Test
{
    [TestClass]
    public class ArrayTest
    {
        [TestMethod]
        public void EmptyTestMethod()
        {
            Array<string>.Empty.Length.Should().Be(0);
        }
    }
}
