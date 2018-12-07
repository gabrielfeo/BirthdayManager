using System;

namespace ConsoleApp.Commands
{
    public interface ICommand
    {
        string Description { get; }
        void Execute();
    }
}