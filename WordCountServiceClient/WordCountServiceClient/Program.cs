using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordCountServiceClient.WordCountServiceReference;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WordCountServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WordCountClient proxy = new WordCountClient("NetNamedPipeBinding_IWordCount"))
            {
                while (proxy.State == CommunicationState.Created || proxy.State == CommunicationState.Opened)
                {
                    Console.WriteLine("Enter your text (or 'q' to quit): ");
                    string input = Console.ReadLine();
                    if (input == "q")
                    {
                        break;
                    }

                    try
                    {
                        proxy.ProcessUserInput(input);
                    }
                    catch (EndpointNotFoundException)
                    {
                    }
                    
                    Console.WriteLine();

                    //Task<string> task = proxy.GetRunningWordCount(input);
                    
                }
            }

            Console.WriteLine("Bye!!!"); 
            Console.ReadLine();
        }
    }
}
