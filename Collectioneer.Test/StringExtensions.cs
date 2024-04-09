using Collectioneer.API.Shared.Infrastructure.Configuration.Extensions;

namespace Collectioneer.Test
{
    public class StringExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("testCase", "test_case")]
        [TestCase("TESTCASE", "testcase")]
        [TestCase("Test Case", "test_case")]
        public void TestMyExtensionMethod(string input, string expected)
        {
            // Act
            var result = input.ToSnakeCase();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}