using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordCountServiceLibrary;
using System.ServiceModel;

namespace WordCountServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WordCountService)))
            {
                host.Open();

                Console.WriteLine("The WordCountService service is ready.");
                Console.WriteLine("Press the Enter key to terminate service.");
                Console.WriteLine();

                Console.ReadLine();
            }
        }
    }
}
