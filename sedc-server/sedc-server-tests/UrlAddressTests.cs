using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sedc.Server.Core;
using System;
using System.Linq;

namespace Sedc.Server.Tests
{
    [TestClass]
    public class UrlAddressTests
    {
        [TestMethod]
        public void URL_Address_Should_Return_Empty_Fields_When_Called_With_An_Empty_String()
        {
            // 1. Arrange
            var input = string.Empty;
            var expected = string.Empty;
            // 2. Act
            var actual = UrlAddress.FromString(input);

            // 3. Arrange
            Assert.AreEqual(0, actual.Path.Count());
            Assert.AreEqual(0, actual.Params.Count);
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void URL_Address_Should_Return_Empty_Fields_When_Called_With_An_String_With_A_Slash()
        {
            // 1. Arrange
            var input = "/";
            var expected = string.Empty;
            // 2. Act
            var actual = UrlAddress.FromString(input);

            // 3. Arrange
            Assert.AreEqual(0, actual.Path.Count());
            Assert.AreEqual(0, actual.Params.Count);
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void URL_Address_Should_Return_Empty_Fields_When_Called_With_An_String_With_Multiple_Slash()
        {
            // 1. Arrange
            var input = "/////";
            var expected = string.Empty;
            // 2. Act
            var actual = UrlAddress.FromString(input);

            // 3. Arrange
            Assert.AreEqual(0, actual.Path.Count());
            Assert.AreEqual(0, actual.Params.Count);
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void URL_Address_Should_Return_Single_Path_Entry_When_Called_With_Symbol_Characters()
        {
            // 1. Arrange
            var input = "<>.:";
            var expected = "<>.:";
            // 2. Act
            var actual = UrlAddress.FromString(input);

            // 3. Arrange
            Assert.AreEqual(1, actual.Path.Count());
            Assert.AreEqual(0, actual.Params.Count);
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void URL_Address_Should_Return_Empty_Path_And_Parameters_When_Called_With_A_Leading_Question_Mark()
        {
            // 1. Arrange
            var input = "?one=1&two=2";
            var expectedString = "?one=1&two=2";
            var expectedOne = new { Key = "one", Value = "1" };
            var expectedTwo = new { Key = "two", Value = "2" };
            // 2. Act
            var actual = UrlAddress.FromString(input);

            // 3. Arrange
            Assert.AreEqual(0, actual.Path.Count());
            Assert.AreEqual(2, actual.Params.Count);

            Assert.IsTrue(actual.Params.ContainsKey(expectedOne.Key));
            Assert.AreEqual(expectedOne.Value, actual.Params[expectedOne.Key]);

            Assert.IsTrue(actual.Params.ContainsKey(expectedTwo.Key));
            Assert.AreEqual(expectedTwo.Value, actual.Params[expectedTwo.Key]);

            Assert.AreEqual(expectedString, actual.ToString());
        }

        [TestMethod]
        public void URL_Address_Should_Return_Single_Path_And_Empty_Param_Field_When_Called_With_Numbers_Only()
        {
            var input1 = "984239842374923";
            var input2 = "000000";
            var expected1 = "984239842374923";
            var expected2 = "000000";

            var actual1 = UrlAddress.FromString(input1);
            var actual2 = UrlAddress.FromString(input2);

            Assert.AreEqual(1, actual1.Path.Count());
            Assert.AreEqual(0, actual1.Params.Count);
            Assert.AreEqual(expected1, actual1.ToString());

            Assert.AreEqual(1, actual2.Path.Count());
            Assert.AreEqual(0, actual2.Params.Count);
            Assert.AreEqual(expected2, actual2.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void URL_Address_Should_Return_AppilcationException_When_Called_With_Multiple_Questionmarks()
        {
            var input1 = "????";
            var input2 = "one/two/three?dfsdf=asd&?dfsd=43tfr";

            UrlAddress.FromString(input1);
            UrlAddress.FromString(input2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void URL_Address_Should_Return_AppilcationException_When_Called_With_More_Than_One_Equality_Sign_In_The_Query_String_Key_Value_Pairs()
        {
            var input = "one/two/three?dfsdf=asd&dfsd=43tfr=fdf";
            UrlAddress.FromString(input);
        }
    }


}
