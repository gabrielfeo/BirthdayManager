using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Entities;
using Repository;
using ConsoleApp.Adapter;
using ConsoleApp.Adapter.Command;
using ConsoleApp.Adapter.PersonNs;
using ConsoleApp.Resources;
using ConsoleApp.Commands;
using ConsoleApp.Commands.Exception;
using ConsoleApp.Commands.List;
using ConsoleApp.Commands.Services;
using ICommand = System.Windows.Input.ICommand;

namespace ConsoleApp
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    internal class ConsoleBirthdayManager
    {
        private TextWriter _consoleTextWriter = Console.Out;
        private TextReader _consoleTextReader = Console.In;
        private IRepository<Person> _personRepository = new RepositoryFactory().NewPersonRepository();
        private ICommandList _commands = new CommandList();
        private IConsoleAdapter<IEnumerable<Person>> _personListAdapter;
        private IConsoleAdapter<ICommandList> _commandListAdapter;
        private CommandReaderService _commandReader;
        private CommandParserService _commandParser;

        public ConsoleBirthdayManager()
        {
            _personListAdapter = new PersonListAdapter(_consoleTextWriter);
            _commandListAdapter = new CommandListAdapter(_consoleTextWriter);
            _commandParser = new CommandParserService(_commands);
            _commandReader = new CommandReaderService(_consoleTextReader, _commandParser);
        }

        public void Start()
        {
            var people = _personRepository.GetAll();

            if (people.Count == 0) WriteEmptyRepositoryMessage();
            else _personListAdapter.Write(people);
            SkipLine();

            _commandListAdapter.Write(_commands);
            try
            {
                var command = _commandReader.ReadCommand();
                _consoleTextWriter.WriteLine($"{command}: {command.Description}");
            }
            catch (InvalidCommandException)
            {
                _consoleTextWriter.WriteLine($"{nameof(InvalidCommandException)} thrown");
            }
            
        }

        private void SkipLine() => _consoleTextWriter.WriteLine();

        private void WriteEmptyRepositoryMessage() => _consoleTextWriter.WriteLine(Messages.ERROR_NO_PEOPLE_ADDED);

    }
}