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
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace ConsoleApp
{
    public class ConsoleBirthdayManager
    {
        internal TextWriter ConsoleTextWriter = Console.Out;
        internal TextReader ConsoleTextReader = Console.In;
        internal IRepository<Person> PersonRepository = new RepositoryFactory().NewPersonRepository();
        internal ICommandList Commands = new CommandList();
        internal IConsoleAdapter<IEnumerable<Person>> PersonListAdapter;
        internal IConsoleAdapter<ICommandList> CommandListAdapter;
        internal CommandReaderService CommandReader;
        internal CommandParserService CommandParser;

        public ConsoleBirthdayManager()
        {
            PersonListAdapter = new PersonListAdapter(ConsoleTextWriter);
            CommandListAdapter = new CommandListAdapter(ConsoleTextWriter);
            CommandParser = new CommandParserService(Commands);
            CommandReader = new CommandReaderService(ConsoleTextReader, CommandParser);
        }

        public void PresentPeople()
        {
            var people = PersonRepository.GetAll();
            if (people.Count == 0) WriteEmptyRepositoryMessage();
            else PersonListAdapter.Write(people);
            ConsoleTextWriter.SkipLine();
        }

        public void WriteEmptyRepositoryMessage()
        {
            ConsoleTextWriter.WriteLine(Messages.Error.NoPeopleAdded);
        }

        public void PresentAllCommands()
        {
            CommandListAdapter.Write(Commands);
        }

        public void PresentAvailableCommands(ICommandList availableCommands)
        {
            CommandListAdapter.Write(availableCommands);
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