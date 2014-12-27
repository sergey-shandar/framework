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
        public void V1Test()
        {
            var c = V1.Value;
            //
            Check(c, 0ul, 0);
            Check(c, 1ul, 1);
            Check(c, 0x7Ful, 0x7F);
            Check(c, 0x80ul, 0x80, 0x01);
            Check(c, 300ul, 0xAC, 0x02);
            Check(c, 624485ul, 0xE5, 0x8E, 0x26);
            Check(c, 0xFFul, 0xFF, 0x01);
            Check(c, 0xFFFFul, 0xFF, 0xFF, 0x03);
            Check(c, 0xFFFFFFul, 0xFF, 0xFF, 0xFF, 0x07);
            Check(c, 0xFFFFFFFFul, 0xFF, 0xFF, 0xFF, 0xFF, 0x0F);
            Check(c, 0xFFFFFFFFFFul, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x1F);
            Check(c, 0xFFFFFFFFFFFFul, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x3F);
            Check(
                c,
                0xFFFFFFFFFFFFFFul, 
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x7F);
            Check(
                c,
                0xFFFFFFFFFFFFFFFFul,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x01);
            Check(c, 0x4000ul, 0x80, 0x80, 0x01);
            Check(c, 0x407Ful, 0xFF, 0x80, 0x01);
        }

        [TestMethod]
        public void V2Test()
        {
            var c = V2.Value;
            //
            Check(c, 0ul, 0);
            Check(c, 1ul, 1);
            Check(c, 0x7Ful, 0x7F);
            Check(c, 0x80ul, 0x80, 0x00);
            Check(c, 300ul, 0xAC, 0x01);
            Check(c, 624485ul, 0xE5, 0x8D, 0x25);
            Check(c, 0xFFul, 0xFF, 0x00);
            Check(c, 0xFFFFul, 0xFF, 0xFE, 0x02);
            Check(c, 0xFFFFFFul, 0xFF, 0xFE, 0xFE, 0x06);
            Check(c, 0xFFFFFFFFul, 0xFF, 0xFE, 0xFE, 0xFE, 0x0E);
            Check(c, 0xFFFFFFFFFFul, 0xFF, 0xFE, 0xFE, 0xFE, 0xFE, 0x1E);
            Check(c, 0xFFFFFFFFFFFFul, 0xFF, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0x3E);
            Check(
                c,
                0xFFFFFFFFFFFFFFul,
                0xFF, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0x7E);
            Check(
                c,
                0xFFFFFFFFFFFFFFFFul,
                0xFF, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0x00);
            Check(c, 0x4000ul, 0x80, 0x7F);
            Check(c, 0x407Ful, 0xFF, 0x7F);
        }

        [TestMethod]
        public void ZigZagTest()
        {
            ZigZagCheck(0, 0);
            ZigZagCheck(-1, 1);
            ZigZagCheck(1, 2);
            ZigZagCheck(-2, 3);
            ZigZagCheck(2147483647, 4294967294);
            ZigZagCheck(-2147483648, 4294967295);
        }

        private static void ZigZagCheck(long signed, ulong unsigned)
        {
            ZigZag.ToUnsigned(signed).Should().Be(unsigned);
            ZigZag.FromUnsigned(unsigned).Should().Be(signed);
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

        private static void Check<T>(
            ICompression<T> compression, T value, params byte[] array)
        {
            compression.Encode(value).Should().BeEquivalentTo(array);
            compression.Decode(Reader(array)).Should().Be(value);
        }
    }
}
