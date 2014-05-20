using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WordCountServiceLibraryX
{
    public interface IWordCountProcessor
    {
        string GetRunningWordCount(string value);
    }

    public class WordCountProcessor : IWordCountProcessor
    {
        private static IDictionary<string, int> wordCountDictionary = new Dictionary<string, int>();
        private object wordCountLock = new object();

        public string GetRunningWordCount(string value)
        {
            var strippedValue = StripPunctuation(value);
            var wordCount = "Count unavailable...";
            
            bool lockTaken = false;
            try
            {
                Monitor.Enter(wordCountLock, ref lockTaken);

                //TEMP
                Thread.Sleep(5000);

                CountWords(strippedValue);
                wordCount = TotalWordCount();
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(wordCountLock);
                }
            }

            //Console.WriteLine("Current running count:");
            //Console.WriteLine(wordCount);
            //Console.WriteLine();
            //TEMP
            Debug.WriteLine(wordCount);

            return wordCount;
        }
        
        private string StripPunctuation(string value)
        {
            return new string(value.Where(c => !char.IsPunctuation(c)).ToArray());
        }
        
        private void CountWords(string value)
        {
            var tokens = value.Trim().Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in tokens)
            {
                int count;
                wordCountDictionary.TryGetValue(word, out count);
                count++;
                wordCountDictionary[word] = count;
            }
        }

        private string TotalWordCount()
        {
            var formattedKvps = wordCountDictionary.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Key + "=" + kvp.Value);
            return string.Join(Environment.NewLine, formattedKvps);
        }

        
    }
}
