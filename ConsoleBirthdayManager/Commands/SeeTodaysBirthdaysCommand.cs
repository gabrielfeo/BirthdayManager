using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleBirthdayManager.Adapter;
using ConsoleBirthdayManager.Adapter.PersonNs;
using ConsoleBirthdayManager.Extensions;
using ConsoleBirthdayManager.Resources;
using Entities;

namespace ConsoleBirthdayManager.Commands
{
    internal class SeeTodaysBirthdaysCommand : Command
    {
        public override string Name { get; } = "See Today's Birthdays";
        public override string Description { get; } = "";

        public IConsoleAdapter<IEnumerable<Person>> Adapter { get; private set; }

        public override IEnumerable<ICommand> Dependencies { get; } = Enumerable.Empty<ICommand>();

        public override void Execute()
        {
            VerifyProperties();
            InitializeAdapter();

            var todaysPeople = Repository.Search(DateTime.Today.ToString());
            if (todaysPeople.Any()) PresentTodaysPeople(todaysPeople);
            else ErrorWriter.WriteLine(Messages.Error.NoBirthdaysToday);
        }

        private void InitializeAdapter()
        {
            Adapter = new PersonListAdapter(Writer, ErrorWriter);
        }

        private void PresentTodaysPeople(IEnumerable<Person> people)
        {
            Writer.WriteLine(Messages.Declaration.TodaysBirthdays);
            Adapter.Write(people);
            Writer.SkipLine();
        }
    }
}