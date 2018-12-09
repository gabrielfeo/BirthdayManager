using System.Collections.Generic;
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

        public override void Execute()
        {
            VerifyProperties();
            InitializeAdapter();
            DeclareActionToUser();
            ListAllPeople();
            Writer.SkipLine();
        }

        private void InitializeAdapter()
        {
            Adapter = new PersonListAdapter(Writer, ErrorWriter);
        }

        private void DeclareActionToUser()
        {
            Writer.WriteLine(Messages.Declaration.ListingPeople);
        }

        private void ListAllPeople()
        {
            var people = Repository.GetAll();
            Adapter.Write(people);
        }
    }
}