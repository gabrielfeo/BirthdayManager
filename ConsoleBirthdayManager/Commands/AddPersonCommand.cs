using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConsoleBirthdayManager.Extensions;
using ConsoleBirthdayManager.Resources;
using Entities;
using Repository;

namespace ConsoleBirthdayManager.Commands
{
    internal class AddPersonCommand : Command
    {
        public override string Name => "Add Birthday";
        public override string Description => "Add somebody\'s birthday to the Birthday Manager";

        public override IEnumerable<ICommand> Dependencies { get; } = Enumerable.Empty<ICommand>();

        public override void Execute()
        {
            VerifyProperties();
            var name = GetPersonName();
            var birthday = GetPersonBirthday();
            var person = new Person(GenerateGuid(), name, birthday);

            Repository.Insert(person, out bool successful);

            Writer.SkipLine();
            if (successful) Writer.WriteLine(Messages.Success.AddedPerson);
            else ErrorWriter.WriteLine(Messages.Error.AddFailed);
        }

        private string GenerateGuid() => Guid.NewGuid().ToString();

        private string GetPersonName()
        {
            Writer.Write("Name: ");
            return Reader.ReadLine();
        }

        private Birthday GetPersonBirthday()
        {
            Writer.Write("Birthday (month/day): ");
            var birthdayString = Reader.ReadLine();
            var successful = DateTime.TryParse(birthdayString, out var birthday);
            return successful ? new Birthday(birthday) : new Birthday();
        }
    }
}