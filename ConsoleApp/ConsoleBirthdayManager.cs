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

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace ConsoleApp
{
    public class ConsoleBirthdayManager
    {
        private IRepository<Person> PersonRepository;
        private IConsoleAdapter<IEnumerable<Person>> PersonListAdapter;
        private IConsoleAdapter<ICommandList> CommandListAdapter;
        private CommandReaderService CommandReader;
        private CommandParserService CommandParser;

        public TextWriter ConsoleTextWriter { get; set; }
        public TextReader ConsoleTextReader { get; set; }
        public ICommandList AllCommands { get; private set; }
        public ICommandList AvailableCommands { get; set; }

        public ConsoleBirthdayManager(TextWriter consoleTextWriter, TextReader consoleTextReader)
        {
            this.ConsoleTextWriter = consoleTextWriter;
            this.ConsoleTextReader = consoleTextReader;

            AllCommands = new CommandList();
            AvailableCommands = AllCommands;
            CommandListAdapter = new CommandListAdapter(ConsoleTextWriter);
            CommandParser = new CommandParserService(AllCommands);
            CommandReader = new CommandReaderService(ConsoleTextReader, CommandParser);

            PersonRepository = new RepositoryFactory().NewPersonRepository();
            PersonListAdapter = new PersonListAdapter(ConsoleTextWriter);
        }

        public void PresentPeople()
        {
            var people = PersonRepository.GetAll();
            PersonListAdapter.Write(people);
            ConsoleTextWriter.SkipLine();
        }

        public void PresentAllCommands()
        {
            CommandListAdapter.Write(AllCommands);
        }

        public void PresentAvailableCommands()
        {
            CommandListAdapter.Write(AvailableCommands);
        }

        public ICommand TryReadingCommand(int maxTries)
        {
            var tries = 0;
            ICommand command;
            do
            {
                command = TryReadingCommand();
                tries++;
            } while (command == null && tries < maxTries);
            return command;
        }

        public ICommand TryReadingCommand()
        {
            try
            {
                var command = CommandReader.ReadCommand();
                ConsoleTextWriter.WriteLine($"You have selected \"{command}\": {command.Description}");
                return command;
            }
            catch (InvalidCommandException)
            {
                ConsoleTextWriter.WriteLine(Messages.Error.InvalidCommand);
                return null;
            }
        }
    }
}