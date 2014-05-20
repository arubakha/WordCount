using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordCountProcessor.Interfaces;

namespace WordCountProcessor
{
    public class InputModifier : IInputModifier
    {
        public string StripPunctuation(string value)
        {
            return new string(value.Where(c => !char.IsPunctuation(c)).ToArray());
        }
    }
}
