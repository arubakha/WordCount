using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using System.Threading;
using WordCountProcessor.Interfaces;
using System.Threading.Tasks;

namespace WordCountProcessor.Test
{
    [TestFixture]
    public class WordCountProcessorTest
    {
        private IInputModifier inputModifierStub;
        private IWordCounter wordCounterStub;

        private WordCountProcessor CreateTarget()
        {
            inputModifierStub = MockRepository.GenerateStub<IInputModifier>();
            wordCounterStub = MockRepository.GenerateStub<IWordCounter>();

            return new WordCountProcessor(inputModifierStub, wordCounterStub);
        }

        [Test]
        public void ProcessUserInput_Calls_StripPunctuation()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            target.ProcessUserInput("Hello, World!");

            //Assert
            inputModifierStub.AssertWasCalled(a => a.StripPunctuation(Arg<string>.Is.Anything));
        }

        [Test]
        public void ProcessUserInput_Calls_WordCounter()
        {
            //Arrange
            var target = CreateTarget();

            //Act
            target.ProcessUserInput("Hello, World!");

            //Assert
            wordCounterStub.AssertWasCalled(a => a.CountWords(Arg<IDictionary<string, int>>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void ProcessUserInput_Handles_Concurrent_Requests_Without_Delay()
        {
            //Arrange
            var target = CreateTarget();

            int delay = 1000;
            wordCounterStub.Stub(a => a.CountWords(Arg<IDictionary<string, int>>.Is.Anything, Arg<string>.Is.Anything)).WhenCalled(invocation => Thread.Sleep(delay));

            //Act
            var before = DateTime.Now;
            Task task1 = target.ProcessUserInput("Hello, World!");
            Task task2 = target.ProcessUserInput("Foo Bar");
            Task task3 = target.ProcessUserInput("Lorem ipsum dolor");
            var after = DateTime.Now;

            Task.WaitAll(task1, task2, task3);
            var afterWait = DateTime.Now;

            //Assert
            Assert.Less(after - before, afterWait - before);
            Assert.Less((after - before).TotalMilliseconds, 10);
            Assert.GreaterOrEqual((afterWait - before).TotalMilliseconds, 3 * delay);
        }

        [Test]
        public void ProcessUserInput_Handles_Underlying_Calculations_For_Concurrent_Requests_Consecutively()
        {
            //Arrange
            var target = CreateTarget();

            int delay = 1000;
            wordCounterStub.Stub(a => a.CountWords(Arg<IDictionary<string, int>>.Is.Anything, Arg<string>.Is.Anything)).WhenCalled(invocation => Thread.Sleep(delay));

            //Act
            var before = DateTime.Now;
            List<DateTime> callTimes = new List<DateTime>();
            Task task1 = target.ProcessUserInput("Hello, World!")
                .ContinueWith(t => callTimes.Add(DateTime.Now));
            Task task2 = target.ProcessUserInput("Foo Bar")
                .ContinueWith(t => callTimes.Add(DateTime.Now));
            Task task3 = target.ProcessUserInput("Lorem ipsum dolor")
                .ContinueWith(t => callTimes.Add(DateTime.Now));
            var after = DateTime.Now;

            Task.WaitAll(task1, task2, task3);
            var afterWait = DateTime.Now;

            //Assert
            Assert.GreaterOrEqual((callTimes[1] - callTimes[0]).TotalMilliseconds, delay);
            Assert.GreaterOrEqual((callTimes[2] - callTimes[1]).TotalMilliseconds, delay);
            Assert.GreaterOrEqual((afterWait - before).TotalMilliseconds, 3 * delay);
        }
        
    }
}
