using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WordCountProcessor.Test.Helpers
{
    [TestFixture]
    public class DictionaryExtensionTest
    {
        private Dictionary<string, int> CreateTarget()
        {
            return new Dictionary<string, int>() { { "foo", 3 }, { "bar", 2 } };
        }

        [Test]
        public void GetWordCountAsNVP_Returns_Whats_Expected()
        {
            //Arrange
            var target = CreateTarget();
            var wordCountDictionaryExpected = new Dictionary<string, int>() { { "foo", 3 }, { "bar", 2 } };

            //Act
            var actualTesult = target.GetWordCountAsNVP();
            var expectedTesult = "bar=2\r\nfoo=3";

            //Assert
            Assert.AreEqual(expectedTesult, actualTesult);
        }

    }
}
