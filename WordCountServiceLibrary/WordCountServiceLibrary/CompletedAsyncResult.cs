using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCountServiceLibrary
{
    public class CompletedAsyncResult<T> : IAsyncResult
    {
        private T data;

        public CompletedAsyncResult(T data)
        {
            this.data = data;
        }

        public T Data
        {
            get { return data; }
        }

        public object AsyncState
        {
            get { return data; }
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool CompletedSynchronously
        {
            get { return true; }
        }

        public bool IsCompleted
        {
            get { return true; }
        }
    }
}
