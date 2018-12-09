using System.Collections;
using System.Collections.Generic;
using System.IO;
using ConsoleBirthdayManager.Extensions;
using ConsoleBirthdayManager.Resources;
using Entities;

namespace ConsoleBirthdayManager.Adapter.PersonNs
{
    internal class PersonListAdapter : PersonAdapter, IConsoleAdapter<IEnumerable<Person>>
    {
        public PersonListAdapter(TextWriter writer, TextWriter errorWriter = null)
            : base(writer, errorWriter)
        {
        }

        public void Write(IEnumerable<Person> people)
        {
            using (var peopleEnumerator = people.GetEnumerator())
            {
                EnumeratePeople(peopleEnumerator);
            }
        }

        private void EnumeratePeople(IEnumerator<Person> peopleEnumerator)
        {
            for (var i = 1; peopleEnumerator.MoveNext(); i++)
            {
                Writer.Write($"  {i} - ");
                base.Write(peopleEnumerator.Current);
            }
        }
    }
}