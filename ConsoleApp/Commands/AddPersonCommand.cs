using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConsoleApp.Resources;
using Entities;
using Repository;

namespace ConsoleApp.Commands
{
    internal class AddPersonCommand : ICommand
    {
        public string Name => "Add Birthday";
        public string Description => "Add somebody\'s birthday to the Birthday Manager";

        public IRepository<Person> Repository { get; set; }
        public TextWriter Writer { get; set; }
        public TextReader Reader { get; set; }

        public void Execute()
        {
            CommandVerifier.Verify(this);
            var name = GetPersonName();
            var birthday = GetPersonBirthday();
            var person = new Person(GenerateGuid(), name, birthday);
            Repository.Insert(person, out bool successful);
            if (!successful) Writer.WriteLine(Messages.Error.GenericError);
        }

        private string GenerateGuid() => Guid.NewGuid().ToString();

        private string GetPersonName()
        {
            Writer.Write("Name: ");
            return Reader.ReadLine();
        }

        private Birthday GetPersonBirthday()
        {
            Writer.Write("Birthday: ");
            var birthdayString = Reader.ReadLine();
            var successful = DateTime.TryParse(birthdayString, out var birthday);
            return successful ? new Birthday(birthday) : new Birthday();
        }
    }
}