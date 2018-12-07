using System.Collections.Generic;
using System.IO;
using Entities;

namespace ConsoleApp.Adapter.PersonNs
{
    internal class PersonListAdapter : PersonAdapter, IConsoleAdapter<IEnumerable<Person>>
    {

        public PersonListAdapter(TextWriter writer) : base(writer) { }

        public void Write(IEnumerable<Person> people)
        {
            using (var peopleEnumerator = people.GetEnumerator())
            {
                for (var i = 1; peopleEnumerator.MoveNext(); i++)
                {
                    Writer.Write($"{i} - ");
                    base.Write(peopleEnumerator.Current);
                }
            }
        }

    }
}