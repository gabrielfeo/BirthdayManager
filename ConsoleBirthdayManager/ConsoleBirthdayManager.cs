using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Entities;
using Repository;
using ConsoleBirthdayManager.Extensions;
using ConsoleBirthdayManager.Adapter;
using ConsoleBirthdayManager.Adapter.Command;
using ConsoleBirthdayManager.Adapter.PersonNs;
using ConsoleBirthdayManager.Commands;
using ConsoleBirthdayManager.Commands.Exceptions;
using ConsoleBirthdayManager.Commands.List;
using ConsoleBirthdayManager.Commands.Services;
using ConsoleBirthdayManager.Resources;
using Validator;
using static Repository.RepositoryFactory;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace ConsoleBirthdayManager
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
        public ICommandList Commands { get; private set; }

        public ConsoleBirthdayManager(TextWriter consoleTextWriter, TextReader consoleTextReader,
                                      TextWriter errorWriter = null)
        {
            this.TextWriter = consoleTextWriter;
            this.TextReader = consoleTextReader;
            this.ErrorWriter = errorWriter ?? TextWriter;

            Commands = new CommandList();
            CommandListAdapter = new CommandListAdapter(TextWriter);
            CommandParser = new CommandParserService(Commands);
            CommandReader = new CommandReaderService(TextReader, CommandParser);

            var personValidator = new ValidatorFactory().NewValidator<Person>();
            PersonRepository = new RepositoryFactory().NewRepository(StorageOption.Filesystem, personValidator);
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

        public void PresentAvailableCommands()
        {
            CommandListAdapter.Write(Commands);
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
                HandleDependenciesOf(command);
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

        private void HandleDependenciesOf(ICommand command)
        {
            foreach (var dependency in command.Dependencies)
            {
                SetUp(dependency);
                dependency.Execute();
            }
        }

        public void DisplayMessage(string message)
        {
            TextWriter.WriteLine(message);
        }
    }
}