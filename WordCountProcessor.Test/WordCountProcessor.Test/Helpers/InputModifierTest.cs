using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WordCountProcessor.Test.Helpers
{
    [TestFixture]
    public class InputModifierTest
    {
        private InputModifier CreateTarget()
        {
            return new InputModifier();
        }

        [Test]
        public void StripPunctuation_Strips_Punctuation()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var actual = target.StripPunctuation("Hello, World!");

            //Assert
            Assert.AreEqual("Hello World", actual);
        }
    }
}
