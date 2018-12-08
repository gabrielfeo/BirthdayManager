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

        public TextWriter TextWriter { get; set; }
        public TextWriter ErrorWriter { get; set; }
        public TextReader TextReader { get; set; }
        public ICommandList AllCommands { get; private set; }
        public ICommandList AvailableCommands { get; set; }

        public ConsoleBirthdayManager(TextWriter consoleTextWriter, TextReader consoleTextReader,
                                      TextWriter errorWriter = null)
        {
            this.TextWriter = consoleTextWriter;
            this.TextReader = consoleTextReader;
            this.ErrorWriter = errorWriter ?? TextWriter;

            AllCommands = new CommandList();
            AvailableCommands = AllCommands;
            CommandListAdapter = new CommandListAdapter(TextWriter);
            CommandParser = new CommandParserService(AllCommands);
            CommandReader = new CommandReaderService(TextReader, CommandParser);

            PersonRepository = new RepositoryFactory().NewRepository<Person>();
            PersonListAdapter = new PersonListAdapter(TextWriter, ErrorWriter);
        }

        public void PresentPeople()
        {
            var people = PersonRepository.GetAll();
            PersonListAdapter.Write(people);
            TextWriter.SkipLine();
        }

        public void PresentAllCommands()
        {
            CommandListAdapter.Write(AllCommands);
            TextWriter.SkipLine();
        }

        public void PresentAvailableCommands()
        {
            CommandListAdapter.Write(AvailableCommands);
            TextWriter.SkipLine();
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
                TextWriter.WriteLine($"You have selected \"{command}\": {command.Description}");
                return command;
            }
            catch (InvalidCommandException)
            {
                ErrorWriter.WriteLine(Messages.Error.InvalidCommand);
                return null;
            }
        }
    }
}