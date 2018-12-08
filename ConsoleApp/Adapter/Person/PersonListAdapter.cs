using System.Collections;
using System.Collections.Generic;
using System.IO;
using ConsoleApp.Extensions;
using ConsoleApp.Resources;
using Entities;

namespace ConsoleApp.Adapter.PersonNs
{
    internal class PersonListAdapter : PersonAdapter, IConsoleAdapter<IEnumerable<Person>>
    {
        public PersonListAdapter(TextWriter writer) : base(writer)
        {
        }

        public void Write(IEnumerable<Person> people)
        {
            using (var peopleEnumerator = people.GetEnumerator())
            {
                if (IsEmpty(peopleEnumerator))
                {
                    WriteEmptyRepositoryMessage();
                }
                else
                {
                    EnumeratePeople(peopleEnumerator);
                }
                Writer.SkipLine();
            }
        }

        private bool IsEmpty(IEnumerator enumerator)
        {
            var isEmpty = !enumerator.MoveNext();
            enumerator.Reset();
            return isEmpty;
        }

        private void WriteEmptyRepositoryMessage()
        {
            Writer.WriteLine(Messages.Error.NoPeopleAdded);
        }

        private void EnumeratePeople(IEnumerator<Person> peopleEnumerator)
        {
            for (var i = 1; peopleEnumerator.MoveNext(); i++)
            {
                Writer.Write($"{i} - ");
                base.Write(peopleEnumerator.Current);
            }
        }
    }
}