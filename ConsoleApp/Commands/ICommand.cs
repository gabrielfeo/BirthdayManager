using System;
using System.Collections.Generic;
using System.IO;
using Entities;
using Repository;

namespace ConsoleApp.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }

        IRepository<Person> Repository { get; set; }
        TextWriter Writer { get; set; }
        TextReader Reader { get; set; }
        
        IEnumerable<ICommand> Dependencies { get; }
        void Execute();
    }
}