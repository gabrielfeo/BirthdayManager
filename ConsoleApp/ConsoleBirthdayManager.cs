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
using ConsoleApp.Commands.Exceptions;
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

        public void PresentIntro()
        {
            var username = Environment.UserName; 
            TextWriter.WriteLine(Messages.Declaration.Welcoming, username);
            TextWriter.SkipLine();
        }

        public void PresentHeader()
        {
            TextWriter.WriteLine(Messages.Declaration.Header);
            TextWriter.SkipLine();
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

        public ICommand AskForCommand(int maxTries)
        {
            var tries = 0;
            ICommand command;
            do
            {
                command = AskForCommand();
                tries++;
            } while (command == null && tries < maxTries);
            return command;
        }

        public ICommand AskForCommand()
        {
            try
            {
                TextWriter.Write(Messages.Instruction.ChooseCommand);
                var command = CommandReader.ReadCommand();
                TextWriter.WriteLine(Messages.Declaration.SelectedCommand, command.Name, command.Description);
                TextWriter.SkipLine();
                return command;
            }
            catch (InvalidCommandException)
            {
                ErrorWriter.WriteLine(Messages.Error.InvalidCommand);
                return null;
            }
        }

        public void Execute(ICommand command)
        {
            if (command != null)
            {
                SetUp(command);
                command.Execute();
            }
            else
            {
                ErrorWriter.WriteLine(Messages.Error.NoCommandToExecute);
            }
        }

        private void SetUp(ICommand command)
        {
            command.Writer = TextWriter;
            command.Reader = TextReader;
            command.Repository = PersonRepository;
        }

        public void DisplayMessage(string message)
        {
            TextWriter.WriteLine(message);
        }
    }
}