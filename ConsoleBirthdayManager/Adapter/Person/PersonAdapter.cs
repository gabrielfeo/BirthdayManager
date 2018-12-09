using System.IO;
using Entities;

namespace ConsoleBirthdayManager.Adapter.PersonNs
{
    internal class PersonAdapter : IConsoleAdapter<Person>
    {
        public TextWriter Writer { get; }
        protected readonly TextWriter ErrorWriter;

        public PersonAdapter(TextWriter writer, TextWriter errorWriter = null)
        {
            Writer = writer;
            ErrorWriter = errorWriter ?? writer;
        }

        public void Write(Person person)
        {
            var personLine = GetFormatted(person);
            Writer.WriteLine(personLine);
        }

        private string GetFormatted(Person person)
        {
            return $"{person.Name}: {person.Birthday.GetNextDate():yyyy MMMM dd}";
        }
    }
}