using System.IO;
using Entities;
using Repository;

namespace ConsoleApp.Commands
{
    internal abstract class Command : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public IRepository<Person> Repository { get; set; }
        public TextWriter Writer { get; set; }
        public TextReader Reader { get; set; }

        private TextWriter _errorWriter;
        public TextWriter ErrorWriter
        {
            get => _errorWriter ?? Writer;
            set => _errorWriter = value;
        }

        public abstract void Execute();
    }
}