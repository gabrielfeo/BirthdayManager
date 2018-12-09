using System.IO;
using ConsoleBirthdayManager.Extensions;
using ConsoleBirthdayManager.Commands.List;
using static ConsoleBirthdayManager.Resources.Messages;

namespace ConsoleBirthdayManager.Adapter.Command
{
    internal class CommandListAdapter : IConsoleAdapter<ICommandList>
    {
        public TextWriter Writer { get; }

        public CommandListAdapter(TextWriter writer)
        {
            Writer = writer;
        }

        public void Write(ICommandList commands)
        {
            foreach (var command in commands)
            {
                var commandKey = commands.IndexOf(command);
                Writer.WriteLine($"{commandKey}. {command.Name}");
            }
        }
    }
}