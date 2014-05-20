using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using WordCountProcessor.Interfaces;

namespace WordCountProcessor
{
    public static class WordCountRegistration
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<InputModifier>().As<IInputModifier>();
            builder.RegisterType<WordCounter>().As<IWordCounter>();
        }
    }
}
