using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Sedc.Server.Core;
using System.Collections.Generic;

namespace Sedc.Server.Tests
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    [TestClass]
    public class JsonSerializerTests
    {
        [TestMethod]
        public void Serialize_Number_Should_Return_String_With_That_Number()
        {
            // 1. Arrange
            var input = 4;
            var expected = "4";
            // 2. Act
            var actual = JsonSerializer.AsString(input);
            // 3. Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_String_Should_Return_The_Same_String_With_Quotes()
        {
            // 1. Arrange
            var input = "string";
            var expected = "\"string\"";
            // 2. Act
            var actual = JsonSerializer.AsString(input);
            // 3. Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_List_Of_Numbers_Should_Return_The_Valid_JSON_Representation()
        {
            // 1. Arrange
            var input = new List<int> { 2, 4, 6, 8, 11 };
            var expected = "[2,4,6,8,11]";
            // 2. Act
            var actual = JsonSerializer.AsString(input);
            // 3. Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_Boolean_Should_Return_The_String_Representation_of_the_Boolean()
        {
            // 1. Arrange
            var input = false;
            var expected = "false";
            // 2. Act
            var actual = JsonSerializer.AsString(input);
            // 3. Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_Anonymous_Object_Should_Return_The_Valid_JSON_Representation()
        {
            // 1.Arrange
            var input = new
            {
                First = 1,
                Second = 3,
                Operation = "Add",
                Result = 4
            };
            var expected = "{\"First\":1,\"Second\":3,\"Operation\":\"Add\",\"Result\":4}";
            // 2. Act
            var actual = JsonSerializer.AsString(input);
            // 3. Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_Named_Object_Should_Return_The_Valid_JSON_Representation()
        {
            // 1.Arrange
            var input = new Person
            {
                FirstName = "Wekoslav",
                LastName = "Stefanovski",
                Age = 0x2D
            };
            var expected = "{\"FirstName\":\"Wekoslav\",\"LastName\":\"Stefanovski\",\"Age\":45}";
            // 2. Act
            var actual = JsonSerializer.AsString(input);
            // 3. Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_Dictionary_Should_Return_The_Valid_JSON_Representation()
        {
            // 1. Arrange
            var input = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 }
            };
            var expected = "{\"one\":1,\"two\":2}";
            // 2. Act
            var actual = JsonSerializer.AsString(input);
            // 3. Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_String_With_Nested_Quotes_Should_Return_The_Same_String_With_Quotes_Escaped()
        {
            // 1. Arrange
            var input = "E. E. \"Doc\" Smith";
            var expected = "\"E. E. \\\"Doc\\\" Smith\"";
            // 2. Act
            var actual = JsonSerializer.AsString(input);
            // 3. Arrange
            Assert.AreEqual(expected, actual);
        }

    }
}



