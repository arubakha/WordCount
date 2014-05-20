using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using Autofac;
using WordCountProcessor.Interfaces;

namespace WordCountServiceLibrary
{
    [ServiceContract]
    public interface IWordCount
    {
        [OperationContract]
        void ProcessUserInput(string value);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetWordCount(string value, AsyncCallback callback, object state);
        string EndGetWordCount(IAsyncResult result);
    }


    public class WordCountService : IWordCount
    {
        private readonly IWordCountProcessor processor;
        private readonly IContainer container;

        public WordCountService()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(WordCountProcessor.WordCountProcessor).Assembly).AsImplementedInterfaces();
            //WordCountProcessor.WordCountRegistration.RegisterTypes(builder);
            container = builder.Build();

            processor = new WordCountProcessor.WordCountProcessor(container.Resolve<IInputModifier>(), 
                                                                    container.Resolve<IWordCounter>());
        }

        //Sync (with async code)
        public void ProcessUserInput(string value)
        {
            processor.ProcessUserInput(value);

            //Task.Factory.StartNew(() => processor.CountWords(value))
            //    .ContinueWith(t => 
            //    {
            //        Console.WriteLine("Current running count:");
            //        Console.WriteLine(t.Result);
            //        Console.WriteLine();
            //    })
            //    .Wait();
        }

        
        //Begin-End async
        public IAsyncResult BeginGetWordCount(string value, AsyncCallback callback, object state)
        {
            var tcs = new TaskCompletionSource<string>(state);
            var task = Task.Factory.StartNew(() => processor.ProcessUserInput(value));
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                    tcs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled)
                    tcs.TrySetCanceled();
                //else
                //    tcs.TrySetResult(t.Result);
 
                if (callback != null)
                    callback(tcs.Task);
            });
            return tcs.Task;

            //var task = Task.Factory.StartNew(() => processor.GetRunningWordCount(value));
            //return new CompletedAsyncResult<string>(task.Result);
        }

        public string EndGetWordCount(IAsyncResult result)
        {
            return ((Task<string>)result).Result;

            //return ((CompletedAsyncResult<string>)result).Data;
        }
    }
}
