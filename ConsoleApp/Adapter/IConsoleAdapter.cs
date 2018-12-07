using System;
using System.IO;

namespace ConsoleApp.Adapter
{
    public interface IConsoleAdapter<T>
    {
        TextWriter Writer { get; }
        void Write(T item);
    }
}