using System.Collections;
using System.Collections.Generic;

namespace ConsoleBirthdayManager.Commands.List
{
    internal class CommandList : ICommandList
    {

        private static IList<ICommand> _commands = new List<ICommand>
        {
            new ListPeopleCommand(),
            new SearchPeopleCommand(),
            new AddPersonCommand(),
            new UpdatePersonNameCommand(),
            new DeletePersonCommand(),
            new ExitCommand()
        };

        public ICommand this[int index] => _commands[index];
        
        int ICommandList.IndexOf(ICommand command) => _commands.IndexOf(command);

        public int Count => _commands.Count;

        public IEnumerator<ICommand> GetEnumerator() => _commands.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }



}
