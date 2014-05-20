using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCountProcessor.Interfaces
{
    public interface IWordCountProcessor
    {
        Task ProcessUserInput(string value);
    }
}
