using System;
using System.IO;
using System.Collections.Generic;
using Entities;

namespace ConsoleApp.Adapter
{
    internal class PeopleConsoleAdapter : IConsoleAdapter<IEnumerable<Person>>
    {

        private readonly TextWriter _writer;
        private readonly PersonConsoleAdapter _personAdapter;

        public PeopleConsoleAdapter(TextWriter writer)
        {
            _writer = writer;
            _personAdapter = new PersonConsoleAdapter(writer);
        }

        public void Write(IEnumerable<Person> people)
        {
            var peopleEnumerator = people.GetEnumerator();
            for (var i = 1; peopleEnumerator.MoveNext(); i++)
            {
                _writer.Write($"{i} - ");
                _personAdapter.Write(peopleEnumerator.Current);
            }
        }

    }
}