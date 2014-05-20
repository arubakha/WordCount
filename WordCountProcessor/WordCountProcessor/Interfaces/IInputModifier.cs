using System;

namespace WordCountProcessor.Interfaces
{
    public interface IInputModifier
    {
        string StripPunctuation(string value);
    }
}
