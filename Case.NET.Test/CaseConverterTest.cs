using Case.NET.Parsing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.NET.Test
{
    [TestClass]
    public class CaseConverterTest
    {

        // TODO: Proper tests
        [TestMethod]
        public void TestToCamelCase()
        {
            string data = "snake_case";

            ConvertedString result = CaseConverter.CamelCase.ConvertCase(data);

            string value = result.Value;

            Assert.AreEqual("snakeCase", value);
        }

        [TestMethod]
        public void TestToPascalCase()
        {

        }
    }
}
