using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Repository;
using ConsoleBirthdayManager.Adapter;
using ConsoleBirthdayManager.Resources;

namespace ConsoleBirthdayManager.Commands.Services
{
    internal class CommandReaderService
    {

        private readonly TextReader _reader;
        private readonly CommandParserService _parser;

        public CommandReaderService(TextReader reader, CommandParserService parser)
        {
            _reader = reader;
            _parser = parser;
        }

        public ICommand ReadCommand()
        {
            var userInput = _reader.ReadLine();
            var command = _parser.ParseFrom(userInput);
            return command;
        }
        
    }
}