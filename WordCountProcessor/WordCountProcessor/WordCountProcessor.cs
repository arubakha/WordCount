using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordCountProcessor.Interfaces;
using System.Collections.Concurrent;

namespace WordCountProcessor
{
    public class WordCountProcessor : IWordCountProcessor
    {
        private static IDictionary<string, int> wordCountDictionary = new Dictionary<string, int>();
        private object wordCountLock = new object();

        private readonly IInputModifier inputModifier;
        private readonly IWordCounter wordCounter;

        public WordCountProcessor(IInputModifier inputModifier, IWordCounter wordCounter)
        {
            this.inputModifier = inputModifier;
            this.wordCounter = wordCounter;
        }

        public Task ProcessUserInput(string value)
        {
            return Task.Factory.StartNew(() => DoCountWords(value));
        }

        private void DoCountWords(string value)
        {
            var strippedInput = inputModifier.StripPunctuation(value);
            lock (wordCountLock)
            {
                wordCounter.CountWords(wordCountDictionary, strippedInput);
            }
        }
    }
}
