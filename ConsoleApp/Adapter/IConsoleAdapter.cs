using System;
using System.IO;

namespace ConsoleApp.Adapter
{
    public interface IConsoleAdapter<T>
    {
        void Write(T item);
    }
}