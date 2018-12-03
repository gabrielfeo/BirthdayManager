using System;
using System.IO;
using Entities;

namespace ConsoleApp.Adapter
{
    internal class PersonConsoleAdapter : IConsoleAdapter<Person>
    {

        private readonly TextWriter _writer;

        public PersonConsoleAdapter(TextWriter writer)
        {
            _writer = writer;
        }

        public void Write(Person person)
        {
            var personLine = GetFormatted(person);
            _writer.WriteLine(personLine);
        }

        private string GetFormatted(Person person)
        {
            return $"{person.Name}  -  {person.Birthday.GetNextDate()}";
        }

    }
}