using System;
using Entities;
using Repository;
using ConsoleApp.Adapter;

namespace ConsoleApp
{
    internal class ConsoleBirthdayManager
    {

        private readonly IRepository<Person> _personRepository = new RepositoryFactory().NewPersonRepository();
        private readonly IConsoleAdapter<Person> _adapter = new PersonConsoleAdapter(Console.Out);

        public void Start()
        {
            foreach (var person in _personRepository.GetAll())
            {
                _adapter.Write(person);
            }
        }
    }
}