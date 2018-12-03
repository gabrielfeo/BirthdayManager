using System;
using System.IO;
using System.Collections.Generic;
using Entities;
using Repository;
using ConsoleApp.Adapter;
using ConsoleApp.Resources;

namespace ConsoleApp
{
    internal class ConsoleBirthdayManager
    {
        private static readonly TextWriter _consoleTextWriter = Console.Out;
        private readonly IRepository<Person> _personRepository = new RepositoryFactory().NewPersonRepository();
        private readonly IConsoleAdapter<IEnumerable<Person>> _adapter = new PeopleConsoleAdapter(_consoleTextWriter);

        public void Start()
        {
            var people = _personRepository.GetAll();
            if (people.Count == 0) WriteEmptyRepositoryMessage();
            else _adapter.Write(people);
        }

        private void WriteEmptyRepositoryMessage() => _consoleTextWriter.WriteLine(Messages.ERROR_NO_PEOPLE_ADDED);

    }
}