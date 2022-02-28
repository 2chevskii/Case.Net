using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;
using FluentAssertions.Primitives;

namespace Case.NET.Extensions.Test
{
    [TestClass]
    public class StringExtensionsTest
    {
        private const string HELLO_WORLD_CAMEL_CASED    = "helloWorld";
        private const string HELLO_WORLD_PASCAL_CASED   = "HelloWorld";
        private const string HELLO_WORLD_TRAIN_CASED    = "Hello-World";
        private const string HELLO_WORLD_KEBAB_CASED    = "hello-world";
        private const string HELLO_WORLD_SNAKE_CASED    = "hello_world";
        private const string HELLO_WORLD_CONSTANT_CASED = "HELLO_WORLD";

        [TestInitialize]
        public void Setup()
        {
            AssertionOptions.FormattingOptions.MaxDepth = 10;
        }

        [DataTestMethod]
        [DataRow("HelloWorld", HELLO_WORLD_CAMEL_CASED)]
        [DataRow("helloWorld", HELLO_WORLD_CAMEL_CASED)]
        [DataRow("hello-world", HELLO_WORLD_CAMEL_CASED)]
        [DataRow("Hello-world", HELLO_WORLD_CAMEL_CASED)]
        [DataRow("Hello-World", HELLO_WORLD_CAMEL_CASED)]
        [DataRow("Hello World", HELLO_WORLD_CAMEL_CASED)]
        [DataRow("hello World", HELLO_WORLD_CAMEL_CASED)]
        [DataRow("hello_World", HELLO_WORLD_CAMEL_CASED)]
        [DataRow("HELLO_WORLD", HELLO_WORLD_CAMEL_CASED)]
        public void ToCamelCaseTest(string value, string expected) =>
            new StringAssertions(value.ToCamelCase()).Subject.Should().Be(expected);

        //[DataTestMethod]
        //public void ToPascalCaseTest(string value, string expected) { }

        //[DataTestMethod]
        //public void ToSnakeCaseTest(string value, string expected) { }

        //[DataTestMethod]
        //public void ToConstantCaseTest(string value, string expected) { }

        //[DataTestMethod]
        //public void ToKebabCaseTest(string value, string expected) { }

        //[DataTestMethod]
        //public void ToTrainCaseTest(string value, string expected) { }
    }
}
