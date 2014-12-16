using System.Collections;
using FluentAssertions;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class OptionalTest
    {
        [TestMethod]
        public void SelectTestMethod()
        {
            "hello".ToOptional().Select(s => true, () => false).Should().BeTrue();
            new Optional<string>.NoValue().Select(s => true, () => false).Should().BeFalse();
        }

        [TestMethod]
        public void EqualsTestMethod()
        {
            "hello".ToOptional().Equals(((object)"hello").ToOptional()).Should().BeTrue();
            "hello".ToOptional().Equals("hello").Should().BeFalse();
            "hello".ToOptional().Equals("three".ToOptional()).Should().BeFalse();
            3.ToOptional().Equals(((object)3).ToOptional()).Should().BeTrue();
            new Optional<int>.NoValue().Equals(new Optional<string>.NoValue()).Should().BeTrue();
        }

        [TestMethod]
        public void HasValueTestMethod()
        {
            ((Optional) ("hello".ToOptional())).HasValue.Should().BeTrue();
            ((Optional) (new Optional<IEnumerable>.NoValue())).HasValue.Should().BeFalse();
        }

        [TestMethod]
        public void GetHashCodeTestMethod()
        {
            "hello".ToOptional().GetHashCode().Should().Be("hello".GetHashCode());
            new Optional<double>.NoValue().GetHashCode().Should().Be(0);
        }

        [TestMethod]
        public void ClassTestMethod()
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
        public void StructTestMethod()
        {
            OptionalStruct<int>().Cast().HasValue.Should().BeFalse();
            OptionalStruct<int>(34).Cast().HasValue.Should().BeTrue();
            OptionalStruct<int>(57).Cast().Select(value => value == 57, () => false).Should().BeTrue();
        }

        private static Optional.Struct<T> OptionalStruct<T>(Optional.Struct<T> value = new Optional.Struct<T>())
            where T : struct
        {
            return value;
        }
    }
}
