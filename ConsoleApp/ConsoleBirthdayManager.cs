using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Entities;
using Repository;
using ConsoleApp.Adapter;
using ConsoleApp.Adapter.Command;
using ConsoleApp.Adapter.PersonNs;
using ConsoleApp.Commands;
using ConsoleApp.Commands.Exception;
using ConsoleApp.Commands.List;
using ConsoleApp.Commands.Services;
using ConsoleApp.Extensions;
using ConsoleApp.Resources;

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
            
            PresentPeople();
            PresentAllCommands();
            var tries = 0;
            ICommand command;
            do
            {
                command = TryReadingCommand();
                tries++;
            } 
            while (command == null && tries < 4);
        }

        
        private void PresentPeople()
        {
            var people = _personRepository.GetAll();
            if (people.Count == 0) WriteEmptyRepositoryMessage();
            else _personListAdapter.Write(people);
            _consoleTextWriter.SkipLine();
        }

        private void WriteEmptyRepositoryMessage()
        {
            _consoleTextWriter.WriteLine(Messages.Error.NoPeopleAdded);            
        }

        private void PresentAllCommands()
        {
            _commandListAdapter.Write(_commands);
        }

        private void PresentAvailableCommands(ICommandList availableCommands)
        {
            _commandListAdapter.Write(availableCommands);
        }

        private ICommand TryReadingCommand()
        {
            try
            {
                var command = _commandReader.ReadCommand();
                _consoleTextWriter.WriteLine($"You have selected \"{command}\": {command.Description}");
                return command;
            }
            catch (InvalidCommandException)
            {
                _consoleTextWriter.WriteLine(Messages.Error.InvalidCommand);
                return null;
            }
        }
        
    }
}