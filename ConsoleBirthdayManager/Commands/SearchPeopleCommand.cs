using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleBirthdayManager.Extensions;
using ConsoleBirthdayManager.Adapter;
using ConsoleBirthdayManager.Adapter.PersonNs;
using ConsoleBirthdayManager.Resources;
using Entities;

namespace ConsoleBirthdayManager.Commands
{
    internal class SearchPeopleCommand : Command
    {
        public override string Name { get; } = "Search Birthdays";
        public override string Description { get; } = "Search all registered birthdays by name or date";

        public override IEnumerable<ICommand> Dependencies { get; } = Enumerable.Empty<ICommand>();
        public IConsoleAdapter<IEnumerable<Person>> ResultsAdapter { get; private set; }

        public override void Execute()
        {
            VerifyProperties();
            InitializeAdapter();
            
            var query = AskForSearchQuery();
            var results = Repository.Search(query);

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

        private void Present(IEnumerable<Person> results)
        {
            Writer.WriteLine(Messages.Success.SearchResults);
            ResultsAdapter.Write(results);
        }
    }
}