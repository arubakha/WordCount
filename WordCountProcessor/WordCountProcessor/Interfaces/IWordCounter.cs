using System;
using System.Collections.Generic;

namespace WordCountProcessor.Interfaces
{
    public interface IWordCounter
    {
        void CountWords(IDictionary<string, int> wordCountDictionary, string value);
    }
}
