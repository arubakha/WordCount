using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using System.Threading;

namespace WordCountProcessor.Test.Helpers
{
    [TestFixture]
    public class WordCounterTest
    {
        private WordCounter CreateTarget()
        {
            return new WordCounter();
        }

        [Test]
        public void CountWords_Populates_CountDictionary()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var wordCountDictionary = new Dictionary<string, int>();
            target.CountWords(wordCountDictionary, "foo bar");

            //Assert
            Assert.AreEqual(2, wordCountDictionary.Count);
        }

        [Test]
        public void CountWords_Handles_Properly_Multiple_Calls()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var wordCountDictionaryExpected = new Dictionary<string, int>() { { "foo", 3 }, { "bar", 2 } };
            var wordCountDictionary = new Dictionary<string, int>();
            target.CountWords(wordCountDictionary, "foo bar");
            target.CountWords(wordCountDictionary, "bar foo");
            target.CountWords(wordCountDictionary, "foo");
            

            //Assert
            Assert.AreEqual(wordCountDictionaryExpected, wordCountDictionary);
        }

        [Test]
        public void CountWords_Ignores_Empty_Spaces()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var wordCountDictionaryExpected = new Dictionary<string, int>() { { "foo", 1 }, { "bar", 1 } };
            var wordCountDictionary = new Dictionary<string, int>();
            target.CountWords(wordCountDictionary, " foo  bar  ");

            //Assert
            Assert.AreEqual(2, wordCountDictionary.Count);
            Assert.AreEqual(wordCountDictionaryExpected, wordCountDictionary);
        }

        [Test]
        public void CountWords_Handles_Empty_Input()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            var wordCountDictionary = new Dictionary<string, int>();
            target.CountWords(wordCountDictionary, "   ");
            target.CountWords(wordCountDictionary, "");

            //Assert
            Assert.AreEqual(0, wordCountDictionary.Count);
        }

        
    }
}
