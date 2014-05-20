using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WordCountProcessor.Interfaces;

namespace WordCountProcessor
{
    public class WordCounter : IWordCounter
    {
        public void CountWords(IDictionary<string, int> wordCountDictionary, string value)
        {
            var tokens = value.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in tokens)
            {
                int count;
                wordCountDictionary.TryGetValue(word, out count);
                count++;
                wordCountDictionary[word] = count;
            }
        }
    }
}
