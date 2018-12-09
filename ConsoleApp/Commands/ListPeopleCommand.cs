using System.Collections.Generic;
using System.Collections.Immutable;
using ConsoleApp.Adapter;
using ConsoleApp.Adapter.PersonNs;
using ConsoleApp.Extensions;
using ConsoleApp.Resources;
using Entities;

namespace ConsoleApp.Commands
{
    internal class ListPeopleCommand : Command
    {
        public override string Name { get; } = "List All Birthdays";
        public override string Description { get; } = "Lists all birthdays currently registered.";

        public IConsoleAdapter<IEnumerable<Person>> Adapter { get; private set; }

        public override IEnumerable<ICommand> Dependencies { get; } = ImmutableList<ICommand>.Empty;

        public override void Execute()
        {
            VerifyProperties();
            if (Repository.GetAll().Count > 0)
            {
                InitializeAdapter();
                ListAllPeople();
            }
            else
            {
                WriteEmptyRepositoryMessage();
            }
        }

        private void InitializeAdapter()
        {
            Adapter = new PersonListAdapter(Writer, ErrorWriter);
        }

        private void WriteEmptyRepositoryMessage()
        {
            ErrorWriter.WriteLine(Messages.Error.NoPeopleAdded);
        }

        private void ListAllPeople()
        {
            Writer.WriteLine(Messages.Declaration.ListingPeople);
            var people = Repository.GetAll();
            Adapter.Write(people);
            Writer.SkipLine();
        }
    }
}