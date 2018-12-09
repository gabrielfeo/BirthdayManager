using System;
using System.IO;

namespace ConsoleBirthdayManager.Adapter
{
    public interface IConsoleAdapter<T>
    {
        TextWriter Writer { get; }
        void Write(T item);
    }
}