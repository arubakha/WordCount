using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCountProcessor
{
    public static class DictionaryExtensions
    {
        public static string GetWordCountAsNVP(this IDictionary<string, int> dictionary)
        {
            var formattedKvps = dictionary.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Key + "=" + kvp.Value);
            return string.Join(Environment.NewLine, formattedKvps);
        }
    }
}
