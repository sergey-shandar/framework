using System;
using System.Collections.Generic;
using Framework.G1;
using Framework.G1.Leb128;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Test
{
    [TestClass]
    public class Leb128Test
    {
        [TestMethod]
        public void V1TestMethod()
        {
            var c = new V1();
            //
            Check(c, 0, 0);
            Check(c, 1, 1);
            Check(c, 0x7F, 0x7F);
            Check(c, 0x80, 0x80, 0x01);
            Check(c, 300, 0xAC, 0x02);
            Check(c, 624485, 0xE5, 0x8E, 0x26);
        }

        [TestMethod]
        public void V2TestMethod()
        {
            var c = new V2();
            //
            Check(c, 0, 0);
            Check(c, 1, 1);
            Check(c, 0x7F, 0x7F);
            Check(c, 0x80, 0x80, 0x00);
            Check(c, 300, 0xAC, 0x01);
            Check(c, 624485, 0xE5, 0x8D, 0x25);
        }

        private static Func<T> Reader<T>(IEnumerable<T> enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            return () =>
            {
                if (!enumerator.MoveNext())
                {
                    throw new IndexOutOfRangeException();
                }
                return enumerator.Current; 
            };
        }

        private static void Check(
            ICompression<ulong> compression, ulong value, params byte[] array)
        {
            compression.Encode(value).Should().BeEquivalentTo(array);
            compression.Decode(Reader(array)).Should().Be(value);
        }
    }
}
