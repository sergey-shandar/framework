using System;
using System.IO;
using FluentAssertions;
using Framework.G1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class CliTest
    {
        private class Interface 
        {      
        }

        private class Interface1
        {
            public static void Method() { }
        }

        [TestMethod]
        public void RunTest()
        {
            var writer = new StringWriter();
            Cli.Run<Interface>(writer, new string[] {}).Should().Be(Cli.Error);
            writer.ToString().Should().BeEmpty();
        }

        [TestMethod]
        public void Run1Test()
        {
            var writer = new StringWriter();
            Cli.Run<Interface1>(writer, new string[] {}).Should().Be(Cli.Error);
            writer.ToString().Should().Be("method\r\n");
        }
    }
}
