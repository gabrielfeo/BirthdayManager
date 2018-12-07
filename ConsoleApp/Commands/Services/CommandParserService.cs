using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Commands.Exception;
using ConsoleApp.Commands.List;

namespace ConsoleApp.Commands.Services
{
    internal class CommandParserService
    {
        public ICommandList Commands { get; set; }

        public CommandParserService(ICommandList commands)
        {
            Commands = commands;
        }

        public ICommand ParseFrom(string commandString)
        {
            try
            {
                var commandKey = int.Parse(commandString);
                var command = Commands[commandKey];
                return command;
            }
            catch (System.Exception ex) when (ex is FormatException
                                              || ex is OverflowException
                                              || ex is ArgumentOutOfRangeException)
            {
                throw new InvalidCommandException();
            }
        }
    }
}