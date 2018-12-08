using System.IO;

namespace ConsoleApp.Adapter.PersonNs
{
    internal class PersonAdapter : IConsoleAdapter<Entities.Person>
    {
        public TextWriter Writer { get; }
        protected readonly TextWriter ErrorWriter;

        public PersonAdapter(TextWriter writer, TextWriter errorWriter = null)
        {
            Writer = writer;
            ErrorWriter = errorWriter ?? writer;
        }

        public void Write(Entities.Person person)
        {
            var personLine = GetFormatted(person);
            Writer.WriteLine(personLine);
        }

        private string GetFormatted(Entities.Person person)
        {
            return $"{person.Name}  -  {person.Birthday.GetNextDate():2018/12/31}";
        }

    }
}