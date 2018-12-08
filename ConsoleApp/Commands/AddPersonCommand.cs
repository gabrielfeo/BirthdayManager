using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConsoleApp.Resources;
using Entities;
using Repository;

namespace ConsoleApp.Commands
{
    internal class AddPersonCommand : Command
    {
        public override string Name => "Add Birthday";
        public override string Description => "Add somebody\'s birthday to the Birthday Manager";

        public override void Execute()
        {
            CommandVerifier.Verify(this);
            var name = GetPersonName();
            var birthday = GetPersonBirthday();
            var person = new Person(GenerateGuid(), name, birthday);
            Repository.Insert(person, out bool successful);
            if (!successful) ErrorWriter.WriteLine(Messages.Error.GenericError);
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