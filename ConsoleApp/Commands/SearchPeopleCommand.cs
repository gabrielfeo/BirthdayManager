using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ConsoleApp.Adapter;
using ConsoleApp.Adapter.PersonNs;
using ConsoleApp.Extensions;
using ConsoleApp.Resources;
using Entities;

namespace ConsoleApp.Commands
{
    internal class SearchPeopleCommand : Command
    {
        public override string Name { get; } = "Search Birthdays";
        public override string Description { get; } = "Search all registered birthdays by name or date";

        public override IEnumerable<ICommand> Dependencies { get; } = ImmutableList<ICommand>.Empty;
        public IConsoleAdapter<IEnumerable<Person>> ResultsAdapter { get; private set; }

        private string _query;

        public override void Execute()
        {
            VerifyProperties();
            InitializeAdapter();
            _query = AskForSearchQuery();
            var results = SearchPeople();

            Writer.SkipLine();
            if (results.Any()) Present(results);
            else ErrorWriter.WriteLine(Messages.Error.SearchGotNoResults);
        }

        private void InitializeAdapter()
        {
            ResultsAdapter = new PersonListAdapter(Writer, ErrorWriter);
        }

        private string AskForSearchQuery()
        {
            Writer.Write(Messages.Instruction.TypeSearchQuery);
            return Reader.ReadLine();
        }


        private IEnumerable<Person> SearchPeople()
        {
            var people = Repository.GetAll();
            var peopleMatchingQueryAsName = people.Where(NameEqualsQuery);
            var peopleMatchingQueryAsBirthday = people.Where(BirthdayEqualsQuery);
            return peopleMatchingQueryAsName.Concat(peopleMatchingQueryAsBirthday);
        }

        private bool NameEqualsQuery(Person person)
        {
            return person.Name.ToLower().Equals(_query.ToLower());
        }

        private bool BirthdayEqualsQuery(Person person)
        {
            var birthday = ParseBirthdayFrom(_query);
            return person.Birthday.Equals(birthday);
        }

        private Birthday ParseBirthdayFrom(string query)
        {
            DateTime.TryParse(query, out var date);
            return new Birthday(date);
        }

        private void Present(IEnumerable<Person> results)
        {
            Writer.WriteLine(Messages.Success.SearchResults);
            ResultsAdapter.Write(results);
        }
    }
}