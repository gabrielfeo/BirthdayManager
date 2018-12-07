using System.IO;

namespace ConsoleApp.Adapter.PersonNs
{
    internal class PersonAdapter : IConsoleAdapter<Entities.Person>
    {

        private readonly TextWriter _writer;
        public TextWriter Writer => _writer;

        public PersonAdapter(TextWriter writer)
        {
            _writer = writer;
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