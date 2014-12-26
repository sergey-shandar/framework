using System.Collections;
using FluentAssertions;
using Framework.G1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class OptionalTest
    {
        [TestMethod]
        public void SelectTest()
        {
            "hello".ToOptional().Select(s => true, () => false).Should().BeTrue();
            new Optional<string>.Absent().Select(s => true, () => false).Should().BeFalse();
        }

        [TestMethod]
        public void EqualsTest()
        {
            "hello".ToOptional().Equals(((object)"hello").ToOptional()).Should().BeTrue();
            "hello".ToOptional().Equals("hello").Should().BeFalse();
            "hello".ToOptional().Equals("three".ToOptional()).Should().BeFalse();
            3.ToOptional().Equals(((object)3).ToOptional()).Should().BeTrue();
            new Optional<int>.Absent().Equals(new Optional<string>.Absent()).Should().BeTrue();
        }

        [TestMethod]
        public void HasValueTest()
        {
            ((Optional) ("hello".ToOptional())).HasValue.Should().BeTrue();
            ((Optional) (new Optional<IEnumerable>.Absent())).HasValue.Should().BeFalse();
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            "hello".ToOptional().GetHashCode().Should().Be("hello".GetHashCode());
            new Optional<double>.Absent().GetHashCode().Should().Be(0);
        }

        [TestMethod]
        public void ClassTest()
        {
            OptionalClass<string>().Cast().HasValue.Should().BeFalse();
            OptionalClass<string>("hello").Cast().HasValue.Should().BeTrue();
            OptionalClass<string>("hello").Cast().Select(value => value == "hello", () => false).Should().BeTrue();
        }

        private static Optional.Class<T> OptionalClass<T>(Optional.Class<T> value = new Optional.Class<T>())
            where T: class
        {
            return value;
        }

        [TestMethod]
        public void StructTest()
        {
            OptionalStruct<int>().Cast().HasValue.Should().BeFalse();
            OptionalStruct<int>(37).Cast().HasValue.Should().BeTrue();
            OptionalStruct<int>(57).Cast().Select(value => value == 57, () => false).Should().BeTrue();
        }

        private static Optional.Struct<T> OptionalStruct<T>(Optional.Struct<T> value = new Optional.Struct<T>())
            where T : struct
        {
            return value;
        }
    }
}
