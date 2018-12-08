using System.IO;
using ConsoleApp.Commands.Exceptions;
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

        protected void VerifyProperties()
        {
            if (Repository is null)
                throw new CommandNotConfiguredException("Repository not configured in Command");
            if (Writer is null)
                throw new CommandNotConfiguredException("Writer not configured in Command");
            if (Reader is null)
                throw new CommandNotConfiguredException("Reader not configured in Command");
        }
    }
}